function StartLoading(element = 'body') {
    $(element).waitMe({
        effect: 'bounce',
        text: 'Please Wait ...',
        bg: 'rgba(255, 255, 255, 0.7)',
        color: '#000'
    });
}

function CloseLoading(element = 'body') {
    $(element).waitMe('hide');
}

function LoadProductModalBody(productId) {
    $.ajax({
        url: "/load-product-modal-body",
        type: "get",
        data: {
            productId: productId
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $("#ProductModalContent").html(response);

            $('#ProductForm').data('validator', null);
            $.validator.unobtrusive.parse('#ProductForm');

            $("#ProductModal").modal("show");
        },
        error: function () {
            CloseLoading();
        }
    });
}

function ProductFormSubmited(response) {
    CloseLoading();
    if (response.status === "Success") {
        swal("Done", "The Operation Has Done Successfully", "success");
        $("#ProductModal").modal("hide");
        $("#ProductDiv").load(location.href + " #ProductDiv");
    } else {
        swal("Error", "Some thing went wrong please try again ...", "error");
    }
}


function DeleteProduct(productId) {
    swal({
            title: "Are you sure ?",
            text: "Once deleted, you will not be able to recover this imaginary file !",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: "/delete-product",
                    type: "get",
                    data: {
                        productId: productId
                    },
                    beforeSend: function () {
                        StartLoading();
                    },
                    success: function (response) {
                        CloseLoading();
                        if (response.status === "Success") {
                            swal("Done", "The Operation Has Done Successfully", "success");
                            $(`#Product-${productId}`).remove();
                        } else {
                            swal("Error", "Some thing went wrong please try again ...", "error");
                        }
                    },
                    error: function () {
                        CloseLoading();
                    }
                });
            } 
        });
}


