// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Materail Create
function programRegistration() {
	debugger;
	var data = {};
	data.AdminProgramId = $("#ProgramName").val();
	data.ModeOfTraining = $("#ModeOfTraining").val();
	data.StartingDate = $("#StartingDate").val();

	$.ajax({
        type: 'POST',
        dataType: 'Json',
		url: '/Students/StudentProgramCreate',
		data:
		{
			studentProgramsViewModel: data
		},
		success: function (result) {
			debugger;
			if (!result.isError) {

				window.location.reload();
			}
			else {
				newErrorAlert(result.msg, url);
			}
		},
		Error: function (ex) {
			errorAlert(ex);
		}
	});
}


   	
