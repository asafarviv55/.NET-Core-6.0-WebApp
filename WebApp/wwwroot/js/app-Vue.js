
const App = {
			  mounted() {
				console.log("enter mounted");

				//this.initialProducts();
				//this.refreshData();
				this.firstLoading();
			},
			updated() {
				console.log("updated");

 
				
			},
			data() {
				return {
					items: [],
					allProducts: [],
					rKey: 0,
					page: 1,
					sortColumn: 1,
					sortDirection: 1,
					filterString: '',
					offset: 1,
					searctStr: '',
					newCode1: '',
					newDescription1: '',
					newName1: '',
					newSell_date1: '',
					newImagePath1: '',
					deleteItemOptional: -1,
					deleteItemsOptional: "",
					totalRows: 20,
					rowsPerPage1: 5,
					numberOfPages: 4,
					file: '',
					fileUploadProductId: -1,
					previewUrl: '',
					thumbId: 0, 
					formData : new FormData()
				}
			},
			methods: {
					firstLoading() {
						//this.reset();
						querystr = "https://localhost:7163/api/Products/GetAllProducts";
						axios.get(querystr).then(response => {

							this.allProducts = response.data.products;
							this.totalRows = response.data.total;
							this.rowsPerPage1 = this.totalRows;
							this.numberOfPages = Math.ceil(this.totalRows / this.rowsPerPage1);
							this.rKey++;
							var x = document.getElementById("numOfPagesSelect");
							$("#numOfPagesSelect").empty();
							var option = document.createElement("option");
							option.text = this.totalRows;
							option.value = this.totalRows;
							option.name = "pages"
							x.add(option);

							const ddNumOfRows = [Math.ceil(this.totalRows / 5), Math.ceil(this.totalRows / 4), Math.ceil(this.totalRows / 3), Math.ceil(this.totalRows / 2)];
							ddNumOfRows.sort();
							for (var i = 0; i < ddNumOfRows.length; i++) {
								option = document.createElement("option");
								option.text = ddNumOfRows[i];
								option.value = ddNumOfRows[i];
								option.name = "pages"
								x.add(option);
							}
						});

					$("#myTable tr").each(function (index) {// index is the position of the current tr in the table ,ele is the tr
						$("td[name='productImage']", this).each(function () {
							$(this).find("img").attr('id', "thumbnil_" + index);
						});
					});


					},
					reset() {
						console.log("reset");
									Object.assign(this.$data, this.$options.data());
					},
					initialProducts() {
						console.log("initialProducts");
						this.reset();
						querystr = "https://localhost:7163/api/Products/InitialProducts";
						axios.get(querystr).then(response => {

							this.allProducts = response.data.products;
							this.totalRows = response.data.total;
							this.rowsPerPage1 = this.totalRows;
							this.numberOfPages = Math.ceil(this.totalRows / this.rowsPerPage1);
							this.rKey++;
							$("#numOfPagesSelect").empty();
							var x = document.getElementById("numOfPagesSelect");
							var option = document.createElement("option");
							option.text = this.totalRows;
							option.value = this.totalRows;
							option.name = "pages"
							x.add(option);

							const ddNumOfRows = [Math.ceil(this.totalRows / 5), Math.ceil(this.totalRows / 4), Math.ceil(this.totalRows / 3), Math.ceil(this.totalRows / 2)];
							ddNumOfRows.sort();
							for (var i = 0; i < ddNumOfRows.length; i++) {
								option = document.createElement("option");
								option.text = ddNumOfRows[i];
								option.value = ddNumOfRows[i];
								option.name = "pages"
								x.add(option);
							}
						});
				},
				handleFileUpload(id, action) {
					this.reset();
					console.log("handleFileUpload id=" + id);
					this.file = event.target.files[0];
					this.thumbId = id;
					if (action == 'add') {
						this.fileUploadProductId = id;
					}
                    else {
						this.fileUploadProductId = $("#editID").val();
                    }
					this.newImagePath1 = "Images/" + event.target.files[0].name;
					console.log(this.newImagePath1);
				},
				showMyImage1(image) {
					console.log("showMyImage1");
					 
					$("#thumbnil_" + id).attr('src', image);
				},
				showMyImage(id,action) { 
					console.log("showMyImage");
					const file = event.target.files[0];
					const url = URL.createObjectURL(file);
					if (id != -1 || this.fileUploadProductId != -1) {
						$("#thumbnil_" + id).attr('src', url);
					}
					else if (action == 'add') {
						$("#addImagePath").attr('src', url); 
					}
					else if (action == 'edit')
					{
						$("#editImagePath").attr('src', url); 
						this.fileUploadProductId = $("#editID").val();
					}
					
				},
				submitFile() {
					console.log("submitFile");
					formData1 = new FormData();
					formData1.append('file', this.file);
					formData1.append('id', this.fileUploadProductId);
					
					 axios.post('https://localhost:7163/api/Products/FileUpload',
						formData1,
						{
							headers: {
								'Content-Type': 'multipart/form-data'
							}
						}
					 ).then(response => {
						console.log('upload file SUCCESS!!');
						this.refreshData();
					})
					.catch(response => {
						console.log('upload file FAILURE!!');
					});
				},
				changeNumOfRows() {
					console.log("changeNumOfRows");
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
					console.log("changePage");
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
					console.log("addRow1");
					this.newCode1 = $('#newCode').val();
					this.newName1 = $('#newName').val();
					this.newDescription1 = $('#newDescription').val();

					querystr = "https://localhost:7163/api/Products/AddNewProduct?code="+this.newCode1+"&name="+this.newName1+"&description="
						+ this.newDescription1 + "&imagePath=" + this.newImagePath1 ;
					axios.get(querystr).then(response => {
						this.refreshData();
					});
				},
				create() {
					console.log("create");
					querystr = "https://localhost:7163/api/Products/InitialProducts";
					axios.get(querystr).then(response => {
						this.refreshData();
					});
				},
			
				sendItemsfromGridToModal(id, code, name, description,sell_date, imagePath) {
					console.log("sendItemsfromGridToModal");
					$("#editID").val(id);
					$("#editCode").val(code);
					$("#editName").val(name) ;
					$("#editDecription").val(description);
					$("#editSellDate").val(sell_date);
					$("#editImagePath").attr('src',imagePath);
				},
		
				submitModalInputs() {
					console.log("submitModalInputs");
					querystr = "https://localhost:7163/api/Products/UpdateProduct?id="
						+ $('#editID').val()
						+ "&code=" + $('#editCode').val()
						+ "&name='" + $('#editName').val() +"'"
						+ "&description='" + $('#editDecription').val() + "'"
						+ "&imagePath='" + this.newImagePath1 + "'";
					console.log(querystr);
					axios.get(querystr).then(response => {
						this.refreshData();
					});
				},



			
				updateDeleteItem(idsAsString) {
					console.log("updateDeleteItem");
					this.formData = new FormData();
					this.formData.append('id', idsAsString);
				},
				updateDeleteItems(idsAsString) {
					console.log("updateDeleteItems");
					this.formData.append('id', idsAsString);
				},
		        deleteItems() {
					console.log("deleteItems");
					axios.post('https://localhost:7163/api/Products/DeleteProducts',
					this.formData,
					{
						headers: {
							'Content-Type': 'multipart/form-data'
						}
					}
					).then(response => {
						console.log('DeleteProduct files SUCCESS!!');
						this.refreshData();
					})
					.catch(response => {
						console.log(response.data.error)
						console.log('DeleteProduct files FAILURE!!');
					});
				},


				sort(col) {
					console.log("sort");
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
					console.log("search");
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
					console.log("refreshData");
					querystr = "https://localhost:7163/api/Products/GetAllProducts";
					axios.get(querystr)
						.then(response => {
							console.log(response.data);
							this.allProducts = response.data.products;
							//this.rKey++;
						}).catch( function() {
							console.log(response.data.error)
							console.log('refreshData  FAILURE!!');
						});
				}
	}
};

const app = Vue.createApp(App);

app.mount("#app");