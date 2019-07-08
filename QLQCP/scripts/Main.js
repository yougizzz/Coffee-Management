function set_order(id)
{
    var number = parseInt(id);
    $.ajax({
        url: '/Manager/CreateOrder?Id_Product=' + number,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {  },
        error: function () {  }
    })
}


function get_order()
{
    $.ajax({
        url: '/Manager/GetAllOrder',
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
                var sum = 0;
                var list = '';
                $.each(data, function (key, item) {
                    list += '<tr>'
                    + '<td>' + item.product.name + '</td>'
                    + '<td>' + item.quantity + '</td>'
                    + '<td>' + item.product.price + '€' + '</td>'
                    + '<td>' + item.product.price * item.quantity + '€' + '</td>'
                    + '<td><Button class="btn btn-danger get-order" onclick="delete_item_order(' + item.product.id + ')">Delete</Button></td>'
                    + '</tr>';
                    sum += item.product.price * item.quantity;
                });
                if (sum > 0)
                {
                    list += '<tr>'
                   + '<td><a href="/Manager/ClearAll" class="btn btn-danger" >Delete All</a></td>'
                   + '<td></td>'
                   + '<td>Total Bill</td><td>' + sum + '€</td></tr>';
                }

                $('#table-order tbody').html(list);
                $('#btn-confirm-bill').show();

        },
        error: function () { }
    })
}


function delete_item_order(id)
{
    var number = parseInt(id);
    $.ajax({
        url: '/Manager/Delete_Specific_Item?Id_Product=' + number,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            var sum = 0;
            var list = '';
            $.each(data, function (key, item) {
                list += '<tr>'
                + '<td>' + item.product.name + '</td>'
                + '<td>' + item.quantity + '</td>'
                + '<td>' + item.product.price + '€' + '</td>'
                + '<td>' + item.product.price * item.quantity + '€' + '</td>'
                + '<td><Button class="btn btn-danger" onclick="delete_item_order(' + item.product.id + ')">Delete</Button></td>'
                + '</tr>';
                sum += item.product.price * item.quantity;
            });
            if (sum > 0) {
                list += '<tr>'
                + '<td><a href="/Manager/ClearAll" class="btn btn-danger" >Delete All</a></td>'
                + '<td></td>'
                + '<td>Total Bill</td><td>' + sum + '</td></tr>';
            }
            
            $('#table-order tbody').html(list);
            $('#btn-confirm-bill').show();
        },
        error: function () { alert('not find') }
    })
}


function confirm_bill()
{
    var id = $('#Customer_Phone').val();
    $.ajax({
        url: '/Manager/CreateBill?Customer_Phone=' + id,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            if (data == 'Good')
            {
                $('#modal-order').hide();
                $('#result-bill').html('<h2 style="color:#4dff4d">CONFIRM SUCCESSFULLY</h2>')
                $('#modal-confirm-bill').show();
            }
            if(data == 'Bad')
            {
                $('#modal-order').hide();
                $('#result-bill').html('<h2 style="color:red">CONFIRM FAILED</h2>')
                $('#modal-confirm-bill').show();
            }
        }
        
    })
}


function set_product(id)
{
    var number = parseInt(id);
    $.ajax({
        url: '/Manager/Get_Product?Id=' + number,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            $('#Id_Product').val(data.id);
            $('#txt-update-product').val(data.name);
            $('#Price').val(data.price);
            $('#modal-update-product').show();
        },
        error: function () { alert('not find') }
    })
}


function set_member(id)
{
    $.ajax({
        url: '/Manager/Get_Member?Id=' + id,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            $('#Id_Member').val(data.id);
            $('#Username').val(data.username);
            $('#Fullname').val(data.fullname);
            $('#Gender').val(data.gender);
            $('#Date_Of_Birth').val(data.date_of_birth);
            $('#ID_Card').val(data.id_card);
            $('#Phone').val(data.phone);
            $('#Email').val(data.email);
            $('#Address').val(data.address);

            $('#modal-update-staff').show();
        },
        error: function () { alert('not find') }
    })
}


function set_img(id)
{
    var number = parseInt(id);
    $('#Id_Product_Image').val(number);
    $('#modal-change-img').show();
}


function set_table(id)
{
    var number = parseInt(id);
    $.ajax({
        url: '/Manager/GetTable?ID=' + number,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            $('#id_table').val(data.id);
            $('#seat').val(data.seats);
            $('#modal-update-table').show();
        },
        error: function () { alert('not find') }
    })
}


function alert_login()
{
    alert('Please login to book table');
}


function get_all_table()
{
    $.ajax({
        url: '/Manager/GetAllTable',
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            lst = '';
            $.each(data, function (key, item) {
                lst += '<tr>'
                + '<td class="text-center">' + item.id + '</td>'
                + '<td class="text-center">' + item.seats + '</td>'
                + '<td class="text-center">'
                + '<button onclick="confirm_booking('+item.id +')" class="btn" style="background-color:#b48f64; color: white;">Book</button>'
                + '</td>'
                + '</tr>';
            });
            $('#book_table tbody').html(lst);
            $('#modal-booking-table').show();
        },
        error: function () { alert('not find') }
    })
}


function confirm_booking(id)
{
    var ID = parseInt(id);
    var result = confirm('Do you want to book this table?');
    if(result)
    {
        $.ajax({
            url: '/MemberShip/Book_Table?ID_Table=' + ID,
            type: 'GET',
            dataType: 'json',
            contentType: "application/json; charset=urf-8",
            success: function (data) {
                location.reload();
                alert('Booking Table Success');
            },
            error: function () {
                alert('Booking Table Failed');
            }
        })
    }
}


function load_membership(page)
{
    $.ajax({
        url: '/Account/GetListMemberShip?page=' + page,
        type: 'GET',
        dataType: 'json',
        contentType: "application/json; charset=urf-8",
        success: function (data) {
            var lst = '';
            $.each(data, function (key, item) {
                lst += '<tr>'
                + '<td class="text-center">' + item.phone_number + '</td>'
                + '<td class="text-center">' + item.fullname + '</td>'
                + '<td class="text-center">' + item.gender + '</td>'
                + '<td class="text-center">' + item.score + '</td>'
                + '<td class="text-center">'
                + '<button class="btn btn-danger">Delete</button>'
                + '</td>'
                + '</tr>';
            });
            $('#list_membership tbody').html(lst);
        }
    })
}