// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Student Program Create
function programRegistration() {
	debugger;
	var data = {};
	data.AdminProgramId = $("#ProgramName").val();
	data.ModeOfTraining = $("#ModeOfTraining").val();
	data.StartingDate = $("#StartingDate").val();
	let studentProgramsViewModel = JSON.stringify(data);
	$.ajax({
        type: 'POST',
        dataType: 'Json',
		url: '/Students/StudentProgramCreate',
		data:
		{
			studentProgramsViewModel: studentProgramsViewModel
		},
		success: function (result) {
			debugger;
			if (!result.isError)
			{
				window.location.reload();
				newSuccessAlert(result.msg, url);
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


//Get program for edit
function editProgram(id) {
	debugger;
	$.ajax({
		type: 'GET',
		url: '/Students/EditStudentProgramRegistration', // we are calling json method
		data: { Id: id },
		dataType: 'json',
		success: function (data) {
			debugger;
			
			if (!data.isError) {
				$("#editProgramId").val(data.id);
				$("#editProgramName").val(data.adminProgramId);
				$("#editModeOfTraining").val(data.modeOfTraining);
				debugger;
				var startinDate = dateToInput(data.startingDate);
				$('#editStartingDate').val(startinDate);
			}
		}
	});
};

function dateToInput(dateString) {

	var now = new Date(dateString);
	var day = ("0" + now.getDate()).slice(-2);
	var month = ("0" + (now.getMonth() + 1)).slice(-2);

	var today = now.getFullYear() + "-" + (month) + "-" + (day);

	return today;
}

// Student Program Edit POST ACTION
function programRegistrationEdit() {
	debugger;
	var data = {};
	data.AdminProgramId = $("#editProgramName").val();
	data.Id = $("#editProgramId").val();
	data.ModeOfTraining = $("#editModeOfTraining").val();
	data.StartingDate = $("#editStartingDate").val();
	let studentProgramsViewModel = JSON.stringify(data);
	$.ajax({
		type: 'POST',
		dataType: 'Json',
		url: '/Students/StudentProgramEdit',
		data:
		{
			studentProgramsViewModel: studentProgramsViewModel
		},
		success: function (result) {
			debugger;
			if (!result.isError)
			{
				window.location.reload();
				newSuccessAlert(result.msg, url);
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

//Get program for Delete
function deleteProgram(id) {
	debugger;
	$.ajax({
		type: 'GET',
		url: '/Students/DeleteStudentProgramRegistration', // we are calling json method
		data: { Id: id },
		dataType: 'json',
		success: function (data) {
			debugger;

			if (!data.isError) {
				$("#deleteProgramId").val(data.id);
			}
		}
	});
};

// Student Program DELETE POST ACTION
function programRegistrationDelete() {
	debugger;
	var data = {};
	data.Id = $("#deleteProgramId").val();
	let studentProgramsViewModel = JSON.stringify(data);
	$.ajax({
		type: 'POST',
		dataType: 'Json',
		url: '/Students/StudentProgramDelete',
		data:
		{
			studentProgramsViewModel: studentProgramsViewModel
		},
		success: function (result) {
			debugger;
			if (!result.isError) {
				window.location.reload();
				newSuccessAlert(result.msg, url);
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

//Get Material for edit
function editMaterail(id) {
	debugger;
	$.ajax({
		type: 'GET',
		url: '/Admin/EditMaterialView', // we are calling json method
		data: { Id: id },
		dataType: 'json',
		success: function (data) {
			debugger;

			if (!data.isError) {
				$("#editMateralId").val(data.id);
				$("#editMaterialName").val(data.adminProgramId);
				$("#editMaterialTitle").val(data.title);
				$('#editMaterialFlowOrder').val(data.flowOrder);
				$('#editMaterialFile').val(data.file);
			}
		}
	});
};

//Material Edit Post
function materialEditPost() {
	debugger;
	var data = {};
	var file = document.getElementById("editMaterialFile").files;
	data.id = $('#editMateralId').val();
	data.AdminProgramId = $('#editMaterialName').val();
	data.Title = $('#editMaterialTitle').val();
	data.FlowOrder = $('#editMaterialFlowOrder').val();
	if (file[0] != null) {
		const reader = new FileReader();
		reader.readAsDataURL(file[0]);
		var base64;
		reader.onload = function () {
			base64 = reader.result;
			debugger;
			if (base64 != "" || base64 != 0) {
				let materialsViewModel = JSON.stringify(data);
				$.ajax({
					type: 'Post',
					dataType: 'Json',
					url: '/Admin/EditMaterialPost',
					data: {
						materialsViewModel: materialsViewModel,
						base64: base64
					},
					success: function (result) {
						debugger;
						if (!result.isError) {
							var url = '/Admin/AdminMaterialIndex'
							successAlertWithRedirect(result.msg, url)
						}
						else {
							errorAlert(result.msg)
						}
					},
					error: function (ex) {
						errorAlert("Error occured try again");
					}
				})
			}
			else {
				errorAlert("Please Enter Details");
			}
		}
	}
}

   	
