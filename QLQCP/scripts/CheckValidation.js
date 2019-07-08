function check_password(username)
{
    var pass = $('#old-password').val();
    $.ajax({
        url: '/Home/Validation_Password?Username=' + username,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            if(data.password != pass)
            {
                $('#validation-password').text('Your password is not correct');
                $('#validation-password').show();
                $('#btn-change-pass').hide();
            }
            else
            {
                $('#validation-password').hide();
                $('#btn-change-pass').show();
            }
        }
    })
}


function check_user(username) {
    $.ajax({
        url: '/Home/Validation_Password?Username=' + username,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
                $('#valid-create-username').text('This username is existed.');
                $('#valid-create-username').show();
                $('#btn-create-account').hide();
        },
        error: function () {
            $('#valid-create-username').hide();
            $('#btn-create-account').show();
        }
    })
}


function check_info(username)
{
    $.ajax({
        url: '/Home/Validation_Password?Username=' + username,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            $('#valid-create-info').hide();
            check_member_exist(username);
        },
        error: function () {
            
            $('#valid-create-info').text('This username is no available.');
            $('#valid-create-info').show();
            $('#btn-create-info').hide();
        }
    })
}


function check_member_exist(username)
{
    $.ajax({
        url: '/Home/Validation_Member?Username=' + username,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            $('#valid-duplicate-info').text('This username is used.');
            $('#valid-duplicate-info').show();
            $('#btn-create-info').hide();
            
        },
        error: function () {
            $('#valid-duplicate-info').hide();
            $('#btn-create-info').show();
        }
    })
}


function check_product(name)
{
    $.ajax({
        url: '/Home/Validation_Product?Productname=' + name,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            $('#valid-create-product').text('This name is used.');
            $('#valid-create-product').show();
            $('#btn-product').hide();

        },
        error: function () {
            $('#valid-create-product').hide();
            $('#btn-product').show();
        }
    })
}


function check_duplicate_product(name)
{
    $.ajax({
        url: '/Home/Validation_Product?Productname=' + name,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            $('#valid-update-product').text('This name is used.');
            $('#valid-update-product').show();
            $('#btn-update-product').hide();

        },
        error: function () {
            $('#valid-update-product').hide();
            $('#btn-update-product').show();
        }
    })
}