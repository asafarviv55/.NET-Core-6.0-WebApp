
const App = {
	mounted() {

		axios.get("https://localhost:7163/api/Products/GetAllProducts")
			.then(response => {
				this.allProducts = response.data.products;
				this.totalRows = response.data.total;
 				this.rowsPerPage1 = 5;
				this.numberOfPages = Math.ceil(this.totalRows /   this.rowsPerPage1);
 				this.rKey++;
				
			})
  	},
	updated() {
		var tbl = document.getElementById("myTable");
		for (let i in tbl.rows) {
			let row = tbl.rows[i];
			row.id = "row_" + i;
		}

	/*	var x = document.getElementById("numOfPagesSelect");
		var option = document.createElement("option");
		var rowsPerPageSel = this.totalRows;

		for (var i = numOptions ; i > numOptions; i--) {
			option = document.createElement("option");
			option.text = rowsPerPageSel;
			option.value = rowsPerPageSel;
			option.name = "pages"
			x.add(option);
			rowsPerPageSel = rowsPerPageSel - 5;
		}*/


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
			offset: 1,
			searctStr: '',
			newCode1: '' ,
			newDescription1: '' ,
			newName1: '' ,
			newSell_date1: '',
			deleteItemOptional: 1,
			totalRows: 20,
			rowsPerPage1: 5,
			numberOfPages:4
		} 
	},
	methods: {
		changeNumOfRows() {
 			if (event.target.value == 0 || event.target.value == undefined) {
 				return;
			}
			this.rowsPerPage1 = event.target.value;
			this.numberOfPages = Math.ceil(this.totalRows / this.rowsPerPage1);
			querystr = "https://localhost:7163/api/Products/Paging?offset=0&rowsPerPage=" + this.rowsPerPage1;
			axios.get(querystr).then(response => {
 				this.allProducts = response.data;
 			});
		},
		changePage(page) {
 			if (page == 1)
				this.offset = 0;
			else {
				this.offset = ((page - 1) * this.rowsPerPage1) ;
			}
 			querystr = "https://localhost:7163/api/Products/Paging?offset=" + this.offset + "&rowsPerPage=" + this.rowsPerPage1;
			axios.get(querystr).then(response => {
 				this.allProducts = response.data;
 			});
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
					this.allProducts = response.data.products;
					this.rKey++;
				})
		}
	}
};

const app = Vue.createApp(App);

app.mount("#app");