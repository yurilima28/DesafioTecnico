<script>
    function validarFormulario(event) {
        event.preventDefault();

    const nomeFabricante = document.getElementById("nomeFabricante").value;
    const paisOrigem = document.getElementById("paisOrigem").value;
    const anoFundacao = document.getElementById("anoFundacao").value;
    const anoAtual = new Date().getFullYear();

    let isValid = true;

        if (nomeFabricante.length > 100) {
        document.getElementById("nomeFabricanteError").textContent = "O nome do fabricante não pode exceder 100 caracteres.";
    isValid = false;
        } else {
        document.getElementById("nomeFabricanteError").textContent = "";
        }

        if (paisOrigem.length > 50) {
        document.getElementById("paisOrigemError").textContent = "O país de origem não pode exceder 50 caracteres.";
    isValid = false;
        } else {
        document.getElementById("paisOrigemError").textContent = "";
        }

        if (anoFundacao > anoAtual) {
        document.getElementById("anoFundacaoError").textContent = "O ano de fundação não pode ser maior que o ano atual.";
    isValid = false;
        } else {
        document.getElementById("anoFundacaoError").textContent = "";
        }

    if (isValid) {
        document.forms[0].submit();
        }
    }
</script>