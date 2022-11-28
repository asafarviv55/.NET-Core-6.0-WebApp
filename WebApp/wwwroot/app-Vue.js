
const App = {
	mounted() {
     // alert("asadf");
		axios.get("https://localhost:7163/api/Products/GetAllProducts")
			.then(response => {
				this.allProducts = response.data;
				this.rKey++;
			})
	},
	data() {
		return {
			items:[],
			allProducts: [],
			rKey: 0,
			message: "Hello Element Plus",
			page: 1,
			sortColumn: 1,
			sortDirection: 1,
			filterString: '',
			offset: 5,
			searctStr: '',
			newCode1: '' ,
			newDescription1: '' ,
			newName1: '' ,
			newSell_date1: ''
		}
	},
	methods: {
		addRow(){
			this.newCode1 = $('#newCode').val();
			this.newName1 = $('#newDescription').val();
			this.newDescription1 = $('#newName').val();
			alert("aaa");
			querystr = "https://localhost:7163/api/Products/AddNewProduct?code="+this.newCode1+"&name="+this.newName1+"&description="
						+this.newDescription1 ;
			axios.get(querystr);
		},
		create() {
			querystr = "https://localhost:7163/api/Products/InitialProducts";
			axios.get(querystr);
			this.refreshData();
		},
		editRow(id) {
			this.newCode1 = $('#newEditCode').val();
			this.newName1 = $('#newEditDescription').val();
			this.newDescription1 = $('#newEditName').val();

			querystr = "https://localhost:7163/api/Products/UpdateProduct?id=" + id + "&code=" + this.newCode1 + "&name=" + this.newName1 + "&description="
				+ this.newDescription1;
			axios.get(querystr);

			},
		deleteItem(id) {
			querystr = "https://localhost:7163/api/Products/DeleteProduct?id=" + id;
			axios.delete(querystr);
			this.refreshData();
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

			this.refreshData();

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