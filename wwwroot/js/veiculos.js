<script>
    function mostrarErro(elementoErro, mensagemErro) {
        elementoErro.textContent = mensagemErro;
    return false;
    }

    function limparErro(elementoErro) {
        elementoErro.textContent = "";
    return true;
    }

    function validarFormulario(event) {
        event.preventDefault();

    const modeloVeiculo = document.getElementById("modeloVeiculo").value.trim();
    const anoFabricacao = parseInt(document.getElementById("anoFabricacao").value, 10);
    const valorVeiculo = parseFloat(document.getElementById("valorVeiculo").value.replace(',', '.'));
    const tipo = document.getElementById("tipo").value;
    const descricao = document.getElementById("descricao").value.trim();
    const fabricanteID = document.getElementById("fabricanteID").value;

    let isValid = true;

    const modeloVeiculoError = document.getElementById("modeloVeiculoError");
        if (modeloVeiculo.length === 0 || modeloVeiculo.length > 100) {
        isValid = mostrarErro(modeloVeiculoError, "O nome do modelo não pode exceder 100 caracteres.");
        } else {
        isValid = limparErro(modeloVeiculoError) && isValid;
        }

    const anoFabricacaoError = document.getElementById("anoFabricacaoError");
    if (isNaN(anoFabricacao) || anoFabricacao < 1900 || anoFabricacao > new Date().getFullYear()) {
        isValid = mostrarErro(anoFabricacaoError, "O ano de fabricação deve ser um ano válido e não pode ser maior que o ano atual.");
        } else {
        isValid = limparErro(anoFabricacaoError) && isValid;
        }

    const valorVeiculoError = document.getElementById("valorVeiculoError");
    if (isNaN(valorVeiculo) || valorVeiculo < 0.01 || valorVeiculo > 9999999999.99) {
        isValid = mostrarErro(valorVeiculoError, "O valor do veículo deve ser um valor entre 0,01 e 9.999.999.999.");
        } else {
        isValid = limparErro(valorVeiculoError) && isValid;
        }

    const tipoError = document.getElementById("tipoError");
    if (tipo === "") {
        isValid = mostrarErro(tipoError, "O tipo de veículo é obrigatório.");
        } else {
        isValid = limparErro(tipoError) && isValid;
        }

    const descricaoError = document.getElementById("descricaoError");
        if (descricao.length > 500) {
        isValid = mostrarErro(descricaoError, "A descrição não pode conter mais de 500 caracteres.");
        } else {
        isValid = limparErro(descricaoError) && isValid;
        }

    const fabricanteIDError = document.getElementById("fabricanteIDError");
    if (fabricanteID === "") {
        isValid = mostrarErro(fabricanteIDError, "O fabricante é obrigatório.");
        } else {
        isValid = limparErro(fabricanteIDError) && isValid;
        }

    if (isValid) {
        document.getElementById("veiculoForm").submit();
        }
    }

    document.getElementById("veiculoForm").addEventListener("submit", validarFormulario);
</script>
