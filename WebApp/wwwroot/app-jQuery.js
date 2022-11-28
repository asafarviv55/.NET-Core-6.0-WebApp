$(document).ready(function(){
	var counter = 0;

	var table = document.getElementById("myTable");
	for (var i = 0, row; row = table.rows[i]; i++) {
		row.id = i;
	}

 

	$("#myTable").on("click", ".ibtnDel", function (event) {
		$("").remove();       
		counter -= 1
	});

});