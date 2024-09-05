document.addEventListener('DOMContentLoaded', function () {
    Inputmask("(99) 99999-9999").mask(document.getElementById('Telefone'));
    Inputmask("99999-999").mask(document.getElementById('CEP'));
});