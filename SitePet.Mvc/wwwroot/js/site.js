// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function insereTexto()
{
    document.getElementById('divTeste').innerHTML = 'Teste inserindo texto.';
}

function insereTexto2() {
    document.getElementById('divTeste2').innerHTML = "Apenas usuarios podem cadastrar um pet, e apenas o mesmo pode editar ou deletar o Pet que cadastrou.";
}



function Mudarestado(el) {
    var display = document.getElementById(el).style.display;
    if (display == "none") {
        document.getElementById(el).style.display = 'block';
       
    }
    else
        document.getElementById(el).style.display = 'none';
}
