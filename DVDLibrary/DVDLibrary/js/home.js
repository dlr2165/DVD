var webAddr = 'http://localhost:44325/';
$(document).ready(function () {



    $('#createdvdbtn').click(function (event) {
        loadDvds();
        showCreateDisplay();
    });
    $("#create-form-button").click(function (event) {
        var haveValidationErrors = checkAndDisplayValidationErrors($('#create-form').find('input'));
        if (haveValidationErrors) {
            return false;
        }
        $.ajax({
            type: 'POST',
            url: webAddr + 'DVDs/',
            data: JSON.stringify({
                title: $('#create-title').val(),
                releaseYear: $('#create-release-year').val(),
                director: $('#create-director').val(),
                rating: $('#create-rating').val(),
                notes: $('#create-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function () {
                $('#errorMessages').empty();
                $('#create-title').val('');
                $('#create-release-year').val('');
                $('#create-director').val('');
                $('#create-rating').val('');
                $('#create-notes').val('');
                $('#searchResults').hide();
                $('#searchResults').empty();
                $('#content').show();
                loadDvds();
                showHomeDisplay();
            },
            error: function () {
                $("#errorMessages")
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text("Error calling web service. Please try again later."));
            }
        });
    });
    $('#create-form-cancel-button').click(function (event) {
        showHomeDisplay();
    });
    $("#searchbtn").click(function (event) {
        cleardvdTable();
        $('#searchResults').empty();
        var haveValidationErrors = checkAndDisplayValidationErrors($("#search").find('input'));
        if (haveValidationErrors) {

            loadDvds();
            showHomeDisplay();
            return false;
        }
        $.ajax({
            type: 'GET',
            url: webAddr + 'dvds/' + $('#dropdownbox').val() + '/' + $('#searchtxtbox').val(),
            success: function (dvdArray) {
                $.each(dvdArray, function (index, dvd) {
                    var title = dvd.title;
                    var releaseYear = dvd.releaseYear;
                    var director = dvd.directorFirstName + " " + dvd.directorLastName;
                    var rating = dvd.rating;
                    var dvdId = dvd.dvdID;


                    var row = '<tr>';
                    row += '<td><a onclick="gotoTitle(' + dvdId + ')">' + title + '</a></td>';
                    row += '<td>' + releaseYear + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td><a onclick="showEditForm(' + dvdId + ')">Edit</a> | <a id="deleteConfirm" onclick="deleteDvd(' + dvdId + ')">Delete</a></td>';
                    row += '</tr>';
                    cleardvdTable();
                    $('#searchResults').append(row);
                    $('#dropdownbox').val('');
                    $('#searchtxtbox').val('');
                    $('#content').hide();
                    $('#searchResults').show();
                    $('#backbtn').show();
                });
            },
            error: function () {
                $("#errorMessages")
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text("Error calling web service. Please try again later."));
            }
        });
    });
    $('#backbtn').click(function (event) {
        $('#dropdownbox').val('');
        $('#searchtxtbox').val('');
        $('#searchResults').hide();
        $('#backbtn').hide();
        $('#content').show();
        loadDvds();
        showHomeDisplay();
    });
    $('#edit-button').click(function (event) {
        var haveValidationErrors = checkAndDisplayValidationErrors($("#edit-form").find('input'));
        if (haveValidationErrors) {
            return false;
        }
        var dvdID = $('#edit-dvd-id').val();
        $.ajax({
            type: 'PUT',
            url: webAddr + 'dvd/' + dvdID,
            data: JSON.stringify({
                dvdID: $('#edit-dvd-id').val(),
                title: $('#edit-title').val(),
                releaseYear: $('#edit-release-year').val(),
                directorID: $('#edit-director').val(),
                ratingID: $('#edit-rating').val(),
                notes: $('#edit-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*'
            },
            'dataType': 'json',
            success: function () {
                $("#errorMessages").empty();
                hideEditForm();
                loadDvds();
                showHomeDisplay();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
                $('#errorMessages')
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text("Error calling web service. Please try again later."));
            }
        });
    });
    $('#edit-cancel-button').click(function (event) {
        showHomeDisplay();
    });
});
// end of on load function
function loadDvds() {
    cleardvdTable();
    var content = $("#content");
    $.ajax({
        type: 'GET',
        url: webAddr + 'DVDs/',
        crossDomain: true,
        success: function (dvdArray) {
            $.each(dvdArray, function (index, dvd) {
                var title = dvd.title;
                var releaseYear = dvd.releaseYear;
                var directorName = dvd.directorFirstName + " " + dvd.directorLastName;
                var rating = dvd.rating;
                var dvdId = dvd.dvdID;
                var row = '<tr>';
                row += '<td><a onclick="gotoTitle(' + dvdId + ')">' + title + '</a></td>';
                row += '<td>' + releaseYear + '</td>';
                row += '<td>' + directorName + '</td>';
                row += '<td>' + rating + '</td>';
                row += '<td><a onclick="showEditForm(' + dvdId + ')">Edit</a> | <a onclick="deleteDvd(' + dvdId + ')">Delete</a></td>';
                row += '</tr>';
                content.append(row);
            });
        },
        error: function () {
            $("#errorMessages")
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text("Error calling web service. Please try again later."));
        }
    });
}
function showCreateDisplay() {
    $('#errorMessages').empty();
    $('#header').hide();
    $('#displayHome').hide();
    $('#editheader').hide();
    $('#editDisplay').hide();
    $('#displayheader').show();
    $('#createDisplay').show();
}
function showHomeDisplay() {
    $('#errorMessages').empty();
    $('#header').show();
    $('#displayHome').show();
    $('#editheader').hide();
    $('#editDisplay').hide();
    $('#displayheader').hide();
    $('#createDisplay').hide();
    $('#detailheader').hide();
    $('#detailTable').hide();
    $('#backbtn').hide();
}
function showEditDisplay() {
    $('#errorMessages').empty();
    $('#header').hide();
    $('#displayHome').hide();
    $('#editheader').show();
    $('#editDisplay').show();
    $('#displayheader').hide();
    $('#createDisplay').hide();
}
function gotoTitle(dvdId) {
    cleardvdTable();
    $('#errorMessages').empty();
    var detailcontent = $('#detailcontent');
    $.ajax({
        type: 'GET',
        url: webAddr + '/dvd/' + dvdId,
        success: function (data, status) {
            $('#headerdetail').val(data.title);
            var title = data.title;
            $('#headerdetail').text(title);
            var releaseYear = data.releaseYear;
            var director = data.director;
            var rating = data.rating;
            var notes = data.notes;
            var table = '<tr>';
            table += '<td>Release Year:</td>';
            table += '<td>' + releaseDate + '</td>';
            table += '</tr><tr>';
            table += '<td>Director:</td>';
            table += '<td>' + director + '</td>';
            table += '</tr><tr>';
            table += '<td>Rating:</td>';
            table += '<td>' + rating + '</td>';
            table += '</tr><tr>';
            table += '<td>Notes:</td>';
            table += '<td>' + notes + '</td>';
            table += '</tr>';
            detailcontent.append(table);
        },
        error: function () {
            $("#errorMessages")
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text("Error calling web service. Please try again later."));
        }
    });
    $('#header').hide();
    $('#displayHome').hide();
    $('#detailheader').show();
    $('#detailTable').show();
}
function showEditForm(dvdId) {
    $('#errorMessages').empty();

    $.ajax({
        type: 'GET',
        url: webAddr + 'dvd/' + dvdId,
        crossDomain: true,

        success: function (data, status) {
            var headeredit = "Edit Dvd: " + data.title;
            $('#headeredit').text(headeredit);
            $('#edit-title').val(data.title);
            $('#edit-release-year').val(data.releaseYear);
            //$('#edit-director-first-name').val(data.directorFirstName);
            //$('#edit-director-middle-name').val(data.directorMiddleName);
            //$('#edit-director-last-name').val(data.directorLastName);
            $('#edit-rating').val(data.rating);
            $('#edit-notes').val(data.notes);
            $('#edit-dvd-id').val(data.dvdID);

            $.each(data.director, function (key, val) {
                var option = document.createElement('option');
                option.text = val.director;
                option.value = val.directorID;
                if (val.directorID === data.directorID)
                { option.selected = true; }
               
                $("#edit-director").append(option);
            });

            $.each(data.ratings, function (key, val) {
                var option = document.createElement('option');
                option.text = val.rating;
                option.value = val.ratingID;
                if (val.ratingID === data.ratingID)
                    { option.selected = true; }
                $("#edit-rating").append(option);
            });


        },
        error: function () {
            $('#errorMessages')
                .append($('<li>'))
                .attr({ class: 'list-group-item list-group-danger' })
                .text('Error calling web service. Please try again later.');
        }
    });
    showEditDisplay();
}
function hideEditForm() {
    $('#errorMessages').empty();
    $('#edit-title').val('');
    $('#edit-release-year').val('');
    $('#edit-director').val('');
    $('#edit-rating').val('');
    $('#edit-notes').val('');
    showHomeDisplay();
}
function deleteDvd(dvdId) {
    $.confirm({
        title: 'Confirmation',
        content: 'Are you sure you want to delete this Dvd from your collection?',
        buttons: {
            confirm: function () {
                $.alert('Title deleted!');
                $.ajax({
                    type: 'DELETE',
                    url: webAddr + '/dvd/' + dvdId,
                    success: function () {
                        loadDvds();
                        showHomeDisplay();
                    },
                    error: function () {
                        loadDvds();
                        showHomeDisplay();
                    }
                });
            },
            cancel: function () {
                $.alert('Canceled!');
            }
        }
    });
}
function cleardvdTable() {
    $("#content").empty();
}
function checkAndDisplayValidationErrors(input) {
    $("#errorMessages").empty();
    var errorMessages = [];
    input.each(function () {
        if (!this.validity.valid) {
            var errorField = $('label[for=' + this.id + ']').text();
            var errorMessage = this.value;
            errorMessages.push('invalid input.');
            $('#backbtn').show();
        }
    });
    if (errorMessages.length > 0) {
        $.each(errorMessages, function (index, message) {
            $('#errorMessages').append($('<li>').attr({ class: 'list-group-item list-group-item-danger' }).text(message));
        });
        return true;
    } else {
        return false;
    }
}
