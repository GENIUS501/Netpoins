

var Loading = Swal.mixin({
    toast: true,
    didOpen: function (toast) {
        Swal.showLoading();
        $(".swal2-backdrop-show").css({ 'width': '100%', 'heigth': '100%' })
        $(".swal2-backdrop-show").css({ 'width': 'auto' })
    },

    backdrop: 'rgba(0,0,0,0.03)',
    customClass: {
        confirmButton: "hdbtn"
    }

});



var Toast = Swal.mixin({
    toast: true,
    position: 'bottom-start',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: function (toast) {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
});