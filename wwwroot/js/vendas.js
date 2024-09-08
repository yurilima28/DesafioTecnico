$(document).ready(function () {
    $('#FabricanteID').change(function () {
        let fabricanteId = $(this).val();  
        let $veiculoSelect = $('#VeiculoID'); 

        $veiculoSelect.empty();

        if (fabricanteId) {
            $.ajax({
                url: '/Vendas/ObterModelosPorFabricante', 
                type: 'GET',
                data: { fabricanteId: fabricanteId },
                success: function (data) {
                    $veiculoSelect.append('<option value="">Selecione um modelo</option>');

                    $.each(data, function (index, modelo) {
                        $veiculoSelect.append($('<option>', {
                            value: modelo.id,
                            text: modelo.nome
                        }));
                    });

                    if (data.length > 0) {
                        $veiculoSelect.prop('disabled', false);
                    } else {
                        $veiculoSelect.append('<option value="">Nenhum modelo disponível</option>').prop('disabled', true);
                    }
                },
                error: function () {
                    alert('Erro ao carregar os modelos.');
                }
            });
        } else {
            $veiculoSelect.append('<option value="">Selecione um fabricante primeiro</option>').prop('disabled', true);
        }
    });
});
