function fnAbreCarteirasHistoricoProc() {
    var atrib = "menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,toolbar=no,";

    var esquerda = (screen.width - 800) / 2;
    var topo = (screen.height - 286) / 2;

    window.open("HistoricoProcessamentoCarteiras", "modalCarteiras", atrib + "width=800,height=286,top=" + topo + ",left=" + esquerda);                  
    //window.showModalDialog('HistoricoProcessamentoCarteiras', 'modalCarteiras', 'dialogHeight:286px;dialogWidth:800px;center:yes;scroll:no;menubar:no;toolbar:no;status:no;modal:yes');        
}

function fnPesquisaCarteiraOrder(order) {
    window.location = "ProcessamentoManual?order=" + order;
}

function fnAbreDetalheProcessamento(codSubTipoFiltro, dtResultado) {
    var params = "parametros=" + codSubTipoFiltro + "," + dtResultado;

    var atrib = "menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,toolbar=no,";

    var esquerda = (screen.width - 800) / 2;
    var topo = (screen.height - 286) / 2;
    var topo1 = (screen.height - 246) / 2;    

    if (codSubTipoFiltro == 16) //Bloqueio de Ativos
        window.open("ProcessamentoResultadoDetalhe?" + params, "modalResultado", atrib + "width=800,height=286,top=" + topo + ",left=" + esquerda);                  
    else
        window.open("ProcessamentoResultadoDetalheAtivo?" + params, "modalResultadoCarteira", atrib + "width=800,height=246,top=" + topo1 + ",left=" + esquerda);                 

//    if (codSubTipoFiltro == 16) //Bloqueio de Ativos
//        window.showModalDialog('ProcessamentoResultadoDetalhe?' + params, 'modalResultado', 'dialogHeight:286px;dialogWidth:800px;center:yes;scroll:no;menubar:no;toolbar:no;status:no;modal:yes');    
//    else
//        window.showModalDialog('ProcessamentoResultadoDetalheAtivo?' + params, 'modalResultadoCarteira', 'dialogHeight:246px;dialogWidth:800px;center:yes;scroll:no;menubar:no;toolbar:no;status:no;modal:yes');                    
}

function fnLiberarBloquear() {
    var paramsLiberar = "";
    var countCheck = 0;

    var checks = document.getElementsByTagName("input");
    for (var i = 0; i < checks.length; i++) {
        if (checks[i].type == "checkbox") {
            if (checks[i].checked) {
                if (paramsLiberar == "")
                    paramsLiberar = document.getElementById('hdnLiberar_' + countCheck).value;
                else
                    paramsLiberar = paramsLiberar + ";" + document.getElementById('hdnLiberar_' + countCheck).value;
            }

            countCheck++;
        }
    }

    $("#btnSalvar").attr("value", "Aguarde...");    

    $('#btnSalvar').prop('disabled', true);
    $('#btnCancelar').prop('disabled', true);

    $("#hdnLiberar").attr("value", paramsLiberar);   
   
    document.formProcessamentoDetalhe.target = "modalResultado";    

    document.formProcessamentoDetalhe.submit();
}

function fnLiberarCarteira() {    
    var checks = document.getElementsByTagName("input");
    for (var i = 0; i < checks.length; i++) {
        if (checks[i].type == "radio") 
        {
            if (checks[i].checked) 
                document.getElementById('hdnLiberar').value = checks[i].value;            
        }
    }

    $("#btnSalvar").attr("value", "Aguarde...");

    $('#btnSalvar').prop('disabled', true);
    $('#btnCancelar').prop('disabled', true);

    document.formProcessamentoDetalheCarteira.target = "modalResultadoCarteira";

    document.formProcessamentoDetalheCarteira.submit();
}

function fnAbreDetalheAtivoProcessamento(codSubTipoFiltro, dtResultado, codAtivo, codTipoAtivo) {
    var params = 'parametros=' + codSubTipoFiltro + ',' + dtResultado + ',' + codAtivo + ',' + codTipoAtivo;

    var atrib = ",menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,toolbar=no";

    var esquerda = ((screen.width - 800) / 2) + 60;    
    var topo = ((screen.height - 246) / 2) + 40;

    window.open("ProcessamentoResultadoDetalheAtivo?" + params, "", "width=800,height=246,top=" + topo + ",left=" + esquerda + atrib);
    //window.showModalDialog('ProcessamentoResultadoDetalheAtivo?' + params, '', 'dialogHeight:246px;dialogWidth:800px;center:yes;scroll:no;menubar:no;toolbar:no;status:no;modal:yes');
}

function fnAbreDetalheEnquadramento(params, acao) {
    var split = params.split('|');    
    
    var codCarteira = split[0].replace(/_/g, " ");       
    var codProcessamento = split[1];
    var dtResultado = split[2];
    var inDiarioMensal = split[3];

    var params = 'parametros=' + codCarteira + ',' + codProcessamento + ',' + dtResultado + ',' + inDiarioMensal + ',' + acao;

    var atrib = ",menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,toolbar=no";

    var esquerda = 0;
    var topo = 0;

    if (acao == "processamento") {
        esquerda = ((screen.width - 800) / 2) + 110;
        topo = ((screen.height - 286) / 2) + 90;
    }
    else {
        esquerda = (screen.width - 800) / 2;
        topo = (screen.height - 286) / 2;
    }

    window.open("../Enquadramento/EnquadramentoDetalhe?" + params, "modalEnquadramento", "width=800,height=286px,top=" + topo + ",left=" + esquerda + atrib);                     
    //window.showModalDialog('EnquadramentoDetalhe?' + params, 'modalEnquadramento', 'dialogHeight:286px;dialogWidth:800px;center:yes;scroll:no;menubar:no;toolbar:no;status:no;modal:yes');    
}

function fnLiberarEnquadramento(acao) {
    var params = null;
    var checks = document.getElementsByTagName("input");
    var dtResultado;

    for (var i = 0; i < checks.length; i++) 
    {
        if (checks[i].type == "checkbox") 
        {
            if (checks[i].checked) 
            {
                if (params == null)
                    params = checks[i].id;
                else
                    params = params + "," + checks[i].id;
            }
        }
    }    

    if (params == null) {
        alert("Selecione uma opção.");
    }
    else {        
        $("#btnSalvar").attr("value", "Aguarde...");

        $('#btnSalvar').prop('disabled', true);
        $('#btnCancelar').prop('disabled', true);

        $("#hdnLiberar").attr("value", params);         

        if (acao == "processamento") 
        {
            document.formEnquadramentoLiberar.action = "../Processamento/ProcessamentResultadoLiberar";
            document.formEnquadramentoLiberar.target = "modalEnquadramento";            
        } 
        else {            
            if ($("#inDiarioMensal").val() == "diario") {                
                document.formEnquadramentoLiberar.action = "../Enquadramento/EnquadramentoDiario";
                document.formEnquadramentoLiberar.target = "EnquadramentoDiarioPage";
            }
            else {                
                document.formEnquadramentoLiberar.action = "../Enquadramento/EnquadramentoMensal";
                document.formEnquadramentoLiberar.target = "EnquadramentoMensalPage";
            }
        }

        alert("Liberação concluída");

        document.formEnquadramentoLiberar.submit();

        window.close();  
    }    
}

function fnCheckAll(obj) {
    var checks = document.getElementsByTagName("input");
    for (var i = 0; i < checks.length; i++) {
        if (checks[i].type == "checkbox") {
            if (obj.checked)
                checks[i].checked = true;
            else
                checks[i].checked = false;
        }
    }
}

function fnSelecionarChks() {
    var params = null;

    var checks = document.getElementsByTagName("input");
    for (var i = 0; i < checks.length; i++) {
        if (checks[i].type == "checkbox") {
            if (checks[i].checked) {
                if (params == null)
                    params = 'parametrosSelecionar=' + checks[i].id;
                else
                    params = params + "," + checks[i].id;
            }
        }
    }

    if (params == null)
        alert("Selecione um registro.");
    else {        
        window.open("../Historico/HistoricoProcessamento?" + params, "historicoProcessamentoPage");     

        window.close();
    }
}

function fnCheckAllCarteira(obj) {
    var checks = document.querySelectorAll("input[type=checkbox]");
    for (var i = 0; i < checks.length; i++) {
        if (checks[i].id != ("chkCarga")) {
            if (obj.checked)
                checks[i].checked = true;
            else
                checks[i].checked = false;
        }
    }
}