// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function initMCE( options) {

    var finalOptions = Object.assign({
        menubar:false,statusbar:false,
        language: 'fr_FR', plugins: "code link hr textcolor", 
        toolbar: "undo redo | styleselect | bold italic strikethrough forecolor backcolor | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | hr link code"
    }, options);
    tinymce.init(finalOptions);
}