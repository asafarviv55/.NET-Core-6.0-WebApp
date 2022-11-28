$(document).ready(function(){
	var counter = 0;

	var table = document.getElementById("myTable");
	for (var i = 0, row; row = table.rows[i]; i++) {
		row.id = i;
	}


	$("#addrow").on("click", function () {
		var newRow = $("<tr id='newRow'>");
		var cols = "";
		cols += '<td><input type="text" class="form-control" value="" id="newCode" name="code"' + counter + '"/></td>';
		cols += '<td><input type="text" class="form-control" value="" id="newName" name="name"' + counter + '"/></td>';
		cols += '<td><input type="text" class="form-control"  value="" id="newDescription" name="description"' + counter + '"/></td>';
		cols += '<td></td>';

		cols += '<td><input type="button" class="ibtnDel btn btn-md btn-danger "  value="Delete"></td>';

		newRow.append(cols);
		$("#myTable").append(newRow);
		counter++;
	});


	$("#myTable").on("click", ".ibtnDel", function (event) {
		$(this).closest("tr").remove();       
		counter -= 1
	});

});