
const App = {
	mounted() {
		axios.get("https://localhost:7163/api/Products/GetAllProducts")
			.then(response => {
				this.allProducts = response.data;
				this.rKey++;
				
			})
	},
	updated() {
		var tbl = document.getElementById("myTable");
		for (let i in tbl.rows) {
			let row = tbl.rows[i];
			row.id = "row_" + i;
		}
	},
	data() {
		return {
			items:[],
			allProducts: [],
			rKey: 0,
 			page: 1,
			sortColumn: 1,
			sortDirection: 1,
			filterString: '',
			offset: 5,
			searctStr: '',
			newCode1: '' ,
			newDescription1: '' ,
			newName1: '' ,
			newSell_date1: '',
			deleteItemOptional: 1,
			totalRows:100			
		}
	},
	methods: {
		paging() {
			alert(event.target.value);
		},
		addRow1() {
			this.newCode1 = $('#newCode').val();
			this.newName1 = $('#newDescription').val();
			this.newDescription1 = $('#newName').val();

			querystr = "https://localhost:7163/api/Products/AddNewProduct?code="+this.newCode1+"&name="+this.newName1+"&description="
						+this.newDescription1 ;
			axios.get(querystr).then(response => {
				this.refreshData();
			});
		},
		create() {
			querystr = "https://localhost:7163/api/Products/InitialProducts";
			axios.get(querystr).then(response => {
				this.refreshData();
			});
		},
		updateDeleteItem(id) {
			this.deleteItemOptional = id;
		},
		sendItemsfromGridToModal(id, code, name, description, sell_date) {
			$("#editID").val(id);
			$("#editCode").val(code);
			$("#editName").val(name) ;
			$("#editDecription").val(description);
		},
		
		submitModalInputs() {
			querystr = "https://localhost:7163/api/Products/UpdateProduct?id="
				+ $('#editID').val()
				+ "&code=" + $('#editCode').val()
				+ "&name=" + $('#editName').val()
				+ "&description=" + $('#editDecription').val();
			axios.get(querystr).then(response => {
				this.refreshData();
			});
		},
		deleteItem() {
			querystr = "https://localhost:7163/api/Products/DeleteProduct?id=" + this.deleteItemOptional;
			 axios.delete(querystr).then(response => {
				this.refreshData();
			})
			
		},
		sort(col) {
			if (this.sortDirection == 1)
				this.sortDirection = 0;
			else
				this.sortDirection = 1;
			var querystr = "https://localhost:7163/api/Products/GetAllProductsOrderBy?orderCol=" + col + "&orderDirection=" + this.sortDirection;

			axios.get(querystr)
				.then(response => {
					this.allProducts = response.data;
					this.rKey++;
 				})
		},
		search(event) {
			this.searctStr = event.target.value;
			if (this.searctStr == '' || this.searctStr == undefined)
				this.refreshData();
			else
				axios.get("https://localhost:7163/api/Products/SearchProducts?str=" + this.searctStr)
					.then(response => {
						this.allProducts = response.data;
						this.rKey++;
					})
		},
		refreshData() {
			querystr = "https://localhost:7163/api/Products/GetAllProducts";
			axios.get(querystr)
				.then(response => {
					this.allProducts = response.data;
					this.rKey++;
				})
		}
	}
};

const app = Vue.createApp(App);

app.mount("#app");