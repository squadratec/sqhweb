(function () {

    var CONST = {
        textoProjeto: '#txtTextoProjeto',
        textoSubProjeto: '#txtTextoSubProjeto',
        textoMotivo: '#txtTextoMotivo',
        textoPapel: '#txtTextoPapel',
        gerente: '#txtGerente',
        dropProjeto: '#ddlprojeto',
        dropSubProjeto: '#ddlsubprojeto',
        dropMotivo: '#ddlmotivo',
        dropPapel: '#ddlpapel',
        mensagem: '#spanMsg',
        colaborador: '#CodColaborador',
        periodo: '#dtperiodo',
        blocopapel: '#divpapel',
        blocosubprojeto: '#divsubprojeto',
        picker: '.formata-date-picker'
    };

    $(document).ready(function () {
        carregaPickerInicial();

        $(CONST.dropSubProjeto).change(function () {
            var subproj = $(this).val();

            if (subproj != 0) {
                $(CONST.textoSubProjeto).val($(this).find("option:selected").text());
                carregaPapeis(subproj);
            }
        });

        $(CONST.dropProjeto).change(function () {
            var projeto = $(this).val();

            if (projeto != 0) {
                $(CONST.textoProjeto).val($(this).find("option:selected").text());

                carregaSubProjeto($(this).val());

                var arr = projeto.split('|');

                var dadosProjeto = {
                    idProjeto: arr[0],
                    NomeDoGerente: arr[1],
                    EmailDoGerente: arr[2],
                    idGerente: arr[3],
                };
                $(CONST.gerente).val(dadosProjeto.NomeDoGerente);
            }
        });

        $(CONST.dropPapel).change(function () {
            var papel = $(this).val();

            if (papel != 0 && papel != "0|0") {
                $(CONST.textoPapel).val($(this).find("option:selected").text());
            }
        });

        $(CONST.dropMotivo).change(function () {
            var motivo = $(this).val();

            if (motivo != 0) {
                $(CONST.textoMotivo).val($(this).find("option:selected").text());
            }
        });

        carregaProjeto();
    });

    function carregaProjeto() {
        var json = {
            data: primeiraDataPicker()
        };

        $.getJSON('/dashboard/projeto', json, function (retorno) {
            if (retorno != null && retorno.projetos.length > 0) {
                $(CONST.mensagem).html('');

                $.each(retorno.projetos, function (index, item) {
                    $(CONST.dropProjeto).append($('<option>', {
                        value: item.value,
                        text: item.text
                    }));
                });

                $.each(retorno.motivos, function (index, item) {
                    $(CONST.dropMotivo).append($('<option>', {
                        value: item.value,
                        text: item.text
                    }));
                });

                $(CONST.colaborador).val(retorno.idColaborador);
            } else {
                $(CONST.mensagem).html('Para esta data não existe projeto e/ou motivos disponíveis.');
            }
        });
    }

    function primeiraDataPicker() {
        var arr = $(CONST.periodo).val().split('-');
        return arr[0].trim();
    }

    function carregaPapeis(subproj) {
        var data = {
            projeto: $(CONST.dropProjeto).val(),
            dataProjeto: primeiraDataPicker(),
            idColaborador: $(CONST.colaborador).val(),
            subprojeto: subproj
        };

        $.getJSON('/dashboard/papeis', data, function (retorno) {
            if (retorno != null && retorno.papeis != null && retorno.papeis.length > 0) {
                $(CONST.mensagem).html('');

                $(CONST.blocopapel).removeClass('hidden');

                $.each(retorno.papeis, function (index, item) {
                    $(CONST.dropPapel).append($('<option>', {
                        value: item.value,
                        text: item.text
                    }));
                });
            }
            else {
                $(CONST.blocopapel).addClass('hidden');
            }
        });
    }

    function carregaSubProjeto(projeto) {
        var data = {
            projeto: projeto,
            idColaborador: $(CONST.colaborador).val(),
            dataProjeto: primeiraDataPicker()
        }

        $.getJSON('/dashboard/subprojeto', data, function (retorno) {
            if (retorno != null && retorno.subprojetos != null && retorno.subprojetos.length > 0) {
                $(CONST.mensagem).html('');
                $(CONST.blocosubprojeto).removeClass('hidden');

                $.each(retorno.subprojetos, function (index, item) {
                    $(CONST.dropSubProjeto).append($('<option>', {
                        value: item.value,
                        text: item.text
                    }));
                });
            }
            else {
                $(CONST.mensagem).html(retorno.msg);
                $(CONST.blocosubprojeto).addClass('hidden');
            }
        });
    }

    function carregaPickerInicial() {
        $(CONST.picker).daterangepicker({
            maxDate: moment().subtract(1, 'days'),
            locale: {
                'format': 'DD/MM/YYYY',
                "applyLabel": "Aplicar",
                "cancelLabel": "Cancelar",
                "daysOfWeek": [
                    "Dom",
                    "Seg",
                    "Ter",
                    "Quar",
                    "Qui",
                    "Sex",
                    "Sab"
                ],
                "monthNames": [
                    "Janeiro",
                    "Fevereiro",
                    "Março",
                    "Abril",
                    "Maio",
                    "Junho",
                    "Julho",
                    "Agosto",
                    "Setembro",
                    "Outubro",
                    "Novembro",
                    "Dezembro"
                ],
            },
        });

        $(CONST.picker).on('apply.daterangepicker', function (ev, picker) {
            carregaProjeto();
        });
    }
})();
