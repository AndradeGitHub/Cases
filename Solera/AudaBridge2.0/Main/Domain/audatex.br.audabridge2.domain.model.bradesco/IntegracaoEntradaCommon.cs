using System.Collections.Generic;

using Newtonsoft.Json;

namespace audatex.br.audabridge2.domain.model.bradesco
{
    public class IntegracaoEntradaCommon
    {
        public string Encargo { get; set; }

        public Sinistro Sinistro { get; set; }
    }

    public class Sinistro 
    {
        [JsonProperty(PropertyName = "INDICADOR-DMO")]       
        public string Aviso { get; set; }
    }
}
//using System.Collections.Generic;
//using System.Xml.Serialization;

//namespace audatex.br.audabridge2.domain.model.bradesco
//{
//    [XmlRoot(ElementName = "IntegracaoEntradaCommon")]
//    public class IntegracaoEntradaCommon
//    {
//        [XmlElement(ElementName = "Encargo")]
//        public string Encargo { get; set; }
//        [XmlElement(ElementName = "Sinistro")]
//        public Sinistro Sinistro { get; set; }
//        [XmlElement(ElementName = "Perito")]
//        public Perito Perito { get; set; }
//        [XmlElement(ElementName = "Apolice")]
//        public Apolice Apolice { get; set; }
//        [XmlElement(ElementName = "Oficina")]
//        public Oficina Oficina { get; set; }
//        [XmlElement(ElementName = "Veiculo")]
//        public Veiculo Veiculo { get; set; }
//        [XmlElement(ElementName = "Orcamento")]
//        public Orcamento Orcamento { get; set; }
//        [XmlElement(ElementName = "OutrosDados")]
//        public OutrosDados OutrosDados { get; set; }
//        [XmlElement(ElementName = "DadosCompulsorios")]
//        public DadosCompulsorios DadosCompulsorios { get; set; }
//        [XmlElement(ElementName = "DadosComplementares")]
//        public DadosComplementares DadosComplementares { get; set; }
//        [XmlElement(ElementName = "DadosAcordosComerciais")]
//        public string DadosAcordosComerciais { get; set; }
//        [XmlElement(ElementName = "CiaSeguros")]
//        public CiaSeguros CiaSeguros { get; set; }
//    }

//    [XmlRoot(ElementName = "Aviso")]
//    public class Aviso
//    {
//        [XmlAttribute(AttributeName = "DT-ABERTURA")]
//        public string DTABERTURA { get; set; }
//        [XmlAttribute(AttributeName = "NM-OPERADOR")]
//        public string NMOPERADOR { get; set; }
//        [XmlAttribute(AttributeName = "DT-VENCIMENTO-VISTORIA")]
//        public string DTVENCIMENTOVISTORIA { get; set; }
//    }

//    [XmlRoot(ElementName = "Reclamante")]
//    public class Reclamante
//    {
//        [XmlElement(ElementName = "EnderecoReclamante")]
//        public string EnderecoReclamante { get; set; }
//        [XmlAttribute(AttributeName = "RELACAO-COM-SEGURADO")]
//        public string RELACAOCOMSEGURADO { get; set; }
//        [XmlAttribute(AttributeName = "NM-RECLAMANTE")]
//        public string NMRECLAMANTE { get; set; }
//        [XmlAttribute(AttributeName = "CPF-CNPJ-RECLAMANTE")]
//        public string CPFCNPJRECLAMANTE { get; set; }
//        [XmlAttribute(AttributeName = "COD-RECLAMANTE")]
//        public string CODRECLAMANTE { get; set; }
//        [XmlAttribute(AttributeName = "COD-OBJETO-SINISTRADO")]
//        public string CODOBJETOSINISTRADO { get; set; }
//        [XmlAttribute(AttributeName = "COMPLEMENTO-OBJETO-SINISTRADO")]
//        public string COMPLEMENTOOBJETOSINISTRADO { get; set; }
//        [XmlAttribute(AttributeName = "DT-NASCIMENTO")]
//        public string DTNASCIMENTO { get; set; }
//        [XmlAttribute(AttributeName = "TEL-PRINCIPAL-RECLAMANTE")]
//        public string TELPRINCIPALRECLAMANTE { get; set; }
//    }

//    [XmlRoot(ElementName = "Terceiros")]
//    public class Terceiros
//    {
//        [XmlAttribute(AttributeName = "NM-TERCEIRO")]
//        public string NMTERCEIRO { get; set; }
//        [XmlAttribute(AttributeName = "ENDERECO")]
//        public string ENDERECO { get; set; }
//        [XmlAttribute(AttributeName = "BAIRRO")]
//        public string BAIRRO { get; set; }
//        [XmlAttribute(AttributeName = "CIDADE")]
//        public string CIDADE { get; set; }
//        [XmlAttribute(AttributeName = "SIG-UF")]
//        public string SIGUF { get; set; }
//        [XmlAttribute(AttributeName = "TELEFONE")]
//        public string TELEFONE { get; set; }
//        [XmlAttribute(AttributeName = "CNPJ-CPF")]
//        public string CNPJCPF { get; set; }
//        [XmlAttribute(AttributeName = "NM-FABRICANTE-VEICULO")]
//        public string NMFABRICANTEVEICULO { get; set; }
//        [XmlAttribute(AttributeName = "NM-VEICULO")]
//        public string NMVEICULO { get; set; }
//        [XmlAttribute(AttributeName = "PLACA")]
//        public string PLACA { get; set; }
//    }

//    [XmlRoot(ElementName = "Sinistro")]
//    public class Sinistro
//    {
//        [XmlElement(ElementName = "Aviso")]
//        public Aviso Aviso { get; set; }
//        [XmlElement(ElementName = "Reclamante")]
//        public Reclamante Reclamante { get; set; }
//        [XmlElement(ElementName = "Terceiros")]
//        public Terceiros Terceiros { get; set; }
//        [XmlAttribute(AttributeName = "INDICADOR-DMO")]
//        public string INDICADORDMO { get; set; }
//        [XmlAttribute(AttributeName = "CODIGO-UTILIZACAO-GUINCHO")]
//        public string CODIGOUTILIZACAOGUINCHO { get; set; }
//        [XmlAttribute(AttributeName = "INDICADOR-VEICULO-PROJETADO")]
//        public string INDICADORVEICULOPROJETADO { get; set; }
//        [XmlAttribute(AttributeName = "DESCRICAO-VEICULO-PROJETADO")]
//        public string DESCRICAOVEICULOPROJETADO { get; set; }
//        [XmlAttribute(AttributeName = "INDICADOR-REPARO-REVELIA")]
//        public string INDICADORREPAROREVELIA { get; set; }
//        [XmlAttribute(AttributeName = "CODIGO-CLASSIFICACAO-SINISTRO")]
//        public string CODIGOCLASSIFICACAOSINISTRO { get; set; }
//        [XmlAttribute(AttributeName = "CODIGO-TIPO-LOGRADOURO-SINISTRO")]
//        public string CODIGOTIPOLOGRADOUROSINISTRO { get; set; }
//        [XmlAttribute(AttributeName = "CODIGO-TIPO-LOGRADOURO-SEGURADO")]
//        public string CODIGOTIPOLOGRADOUROSEGURADO { get; set; }
//        [XmlAttribute(AttributeName = "CODIGO-TIPO-LOGRADOURO-TERCEIRO")]
//        public string CODIGOTIPOLOGRADOUROTERCEIRO { get; set; }
//        [XmlAttribute(AttributeName = "REPARTICAO-BO")]
//        public string REPARTICAOBO { get; set; }
//        [XmlAttribute(AttributeName = "DATA-VISTORIA-MOVEL")]
//        public string DATAVISTORIAMOVEL { get; set; }
//        [XmlAttribute(AttributeName = "COMPLEMENTO-VISTORIAL-MOVEL")]
//        public string COMPLEMENTOVISTORIALMOVEL { get; set; }
//        [XmlAttribute(AttributeName = "NOME-RESPONSAVEL-ACIDENTE")]
//        public string NOMERESPONSAVELACIDENTE { get; set; }
//        [XmlAttribute(AttributeName = "DDD-PRINCIPAL-RESPONSAVEL-ACIDENTE")]
//        public string DDDPRINCIPALRESPONSAVELACIDENTE { get; set; }
//        [XmlAttribute(AttributeName = "TELEFONE-PRINCIPAL-RESPONSAVEL-ACIDENTE")]
//        public string TELEFONEPRINCIPALRESPONSAVELACIDENTE { get; set; }
//        [XmlAttribute(AttributeName = "DDD-SECUNDARIO-RESPONSAVEL-ACIDENTE")]
//        public string DDDSECUNDARIORESPONSAVELACIDENTE { get; set; }
//        [XmlAttribute(AttributeName = "TELEFONE-SECUNDARIO-RESPONSAVEL-ACIDENTE")]
//        public string TELEFONESECUNDARIORESPONSAVELACIDENTE { get; set; }
//        [XmlAttribute(AttributeName = "RESPONSAVEL-PROCESSO")]
//        public string RESPONSAVELPROCESSO { get; set; }
//        [XmlAttribute(AttributeName = "EMAIL-RESPONSAVEL-PROCESSO")]
//        public string EMAILRESPONSAVELPROCESSO { get; set; }
//        [XmlAttribute(AttributeName = "DDD-PRINCIPAL-RESPONSAVEL-PROCESSO")]
//        public string DDDPRINCIPALRESPONSAVELPROCESSO { get; set; }
//        [XmlAttribute(AttributeName = "TELEFONE-PRINCIPAL-RESPONSAVEL-PROCESSO")]
//        public string TELEFONEPRINCIPALRESPONSAVELPROCESSO { get; set; }
//        [XmlAttribute(AttributeName = "DDD-SECUNDARIO-RESPONSAVEL-PROCESSO")]
//        public string DDDSECUNDARIORESPONSAVELPROCESSO { get; set; }
//        [XmlAttribute(AttributeName = "TELEFONE-SECUNDARIO-RESPONSAVEL-PROCESSO")]
//        public string TELEFONESECUNDARIORESPONSAVELPROCESSO { get; set; }
//        [XmlAttribute(AttributeName = "PREFERENCIA-CONTATO-RESPONSAVEL-PROCESSO")]
//        public string PREFERENCIACONTATORESPONSAVELPROCESSO { get; set; }
//        [XmlAttribute(AttributeName = "CODIGO-PAIS")]
//        public string CODIGOPAIS { get; set; }
//        [XmlAttribute(AttributeName = "TP-PROCESSO")]
//        public string TPPROCESSO { get; set; }
//        [XmlAttribute(AttributeName = "DT-ACIDENTE")]
//        public string DTACIDENTE { get; set; }
//        [XmlAttribute(AttributeName = "NATUREZA")]
//        public string NATUREZA { get; set; }
//        [XmlAttribute(AttributeName = "LOCAL")]
//        public string LOCAL { get; set; }
//        [XmlAttribute(AttributeName = "BAIRRO")]
//        public string BAIRRO { get; set; }
//        [XmlAttribute(AttributeName = "CIDADE")]
//        public string CIDADE { get; set; }
//        [XmlAttribute(AttributeName = "SIG-UF")]
//        public string SIGUF { get; set; }
//        [XmlAttribute(AttributeName = "NUM-QTDE-TERCEIRO")]
//        public string NUMQTDETERCEIRO { get; set; }
//        [XmlAttribute(AttributeName = "INDICADOR_CULPA")]
//        public string INDICADOR_CULPA { get; set; }
//        [XmlAttribute(AttributeName = "ENDERECO-LOCAL-VEICULO")]
//        public string ENDERECOLOCALVEICULO { get; set; }
//        [XmlAttribute(AttributeName = "BAIRRO-LOCAL-VEICULO")]
//        public string BAIRROLOCALVEICULO { get; set; }
//        [XmlAttribute(AttributeName = "CIDADE-LOCAL-VEICULO")]
//        public string CIDADELOCALVEICULO { get; set; }
//        [XmlAttribute(AttributeName = "SIG-UF-LOCAL-VEICULO")]
//        public string SIGUFLOCALVEICULO { get; set; }
//        [XmlAttribute(AttributeName = "TELEFONE-LOCAL-VEICULO")]
//        public string TELEFONELOCALVEICULO { get; set; }
//        [XmlAttribute(AttributeName = "DT-ENTRADA-LOCAL")]
//        public string DTENTRADALOCAL { get; set; }
//        [XmlAttribute(AttributeName = "SETOR-REGISTRO")]
//        public string SETORREGISTRO { get; set; }
//        [XmlAttribute(AttributeName = "INDICADOR-POLICIA")]
//        public string INDICADORPOLICIA { get; set; }
//        [XmlAttribute(AttributeName = "INDICADOR-PERICIA")]
//        public string INDICADORPERICIA { get; set; }
//        [XmlAttribute(AttributeName = "DSC-ACIDENTE")]
//        public string DSCACIDENTE { get; set; }
//        [XmlAttribute(AttributeName = "DSC-AVARIAS")]
//        public string DSCAVARIAS { get; set; }
//        [XmlAttribute(AttributeName = "INDICADOR-RECONHECIMENTO-CULPA")]
//        public string INDICADORRECONHECIMENTOCULPA { get; set; }
//        [XmlAttribute(AttributeName = "NUM-SINISTRO")]
//        public string NUMSINISTRO { get; set; }
//        [XmlAttribute(AttributeName = "FLG-SINISTRO-PREMATURO")]
//        public string FLGSINISTROPREMATURO { get; set; }
//        [XmlAttribute(AttributeName = "DT-AVISO")]
//        public string DTAVISO { get; set; }
//        [XmlAttribute(AttributeName = "NR-ATENDIMENTO")]
//        public string NRATENDIMENTO { get; set; }
//    }

//    [XmlRoot(ElementName = "Perito")]
//    public class Perito
//    {
//        [XmlAttribute(AttributeName = "CNPJ-MEDIADORA")]
//        public string CNPJMEDIADORA { get; set; }
//        [XmlAttribute(AttributeName = "CPFCNPJ-REGUL")]
//        public string CPFCNPJREGUL { get; set; }
//        [XmlAttribute(AttributeName = "GRUPO-REGUL")]
//        public string GRUPOREGUL { get; set; }
//    }

//    [XmlRoot(ElementName = "Segurado")]
//    public class Segurado
//    {
//        [XmlElement(ElementName = "TelComercial")]
//        public string TelComercial { get; set; }
//        [XmlAttribute(AttributeName = "TEL-PRINCIPAL-SEGURADO")]
//        public string TELPRINCIPALSEGURADO { get; set; }
//        [XmlAttribute(AttributeName = "SIG-CHASSI-SEGURADO")]
//        public string SIGCHASSISEGURADO { get; set; }
//        [XmlAttribute(AttributeName = "SIG-UF-SEGURADO")]
//        public string SIGUFSEGURADO { get; set; }
//        [XmlAttribute(AttributeName = "NM-SEGURADO")]
//        public string NMSEGURADO { get; set; }
//        [XmlAttribute(AttributeName = "LOGRAD-SEGURADO")]
//        public string LOGRADSEGURADO { get; set; }
//        [XmlAttribute(AttributeName = "CPF-CNPJ-SEGURADO")]
//        public string CPFCNPJSEGURADO { get; set; }
//        [XmlAttribute(AttributeName = "CIDADE-SEGURADO")]
//        public string CIDADESEGURADO { get; set; }
//        [XmlAttribute(AttributeName = "CEP-SEGURADO")]
//        public string CEPSEGURADO { get; set; }
//        [XmlAttribute(AttributeName = "BAIRRO-SEGURADO")]
//        public string BAIRROSEGURADO { get; set; }
//    }

//    [XmlRoot(ElementName = "Condutor")]
//    public class Condutor
//    {
//        [XmlAttribute(AttributeName = "ESTADO-CIVIL-CONDUTOR")]
//        public string ESTADOCIVILCONDUTOR { get; set; }
//        [XmlAttribute(AttributeName = "NM-CONDUTOR")]
//        public string NMCONDUTOR { get; set; }
//        [XmlAttribute(AttributeName = "NUM-CNH")]
//        public string NUMCNH { get; set; }
//        [XmlAttribute(AttributeName = "COD-CATEGORIA-CNH")]
//        public string CODCATEGORIACNH { get; set; }
//        [XmlAttribute(AttributeName = "DT-VALIDADE-CNH")]
//        public string DTVALIDADECNH { get; set; }
//        [XmlAttribute(AttributeName = "NUM-IDADE-CONDUTOR-RENACH")]
//        public string NUMIDADECONDUTORRENACH { get; set; }
//        [XmlAttribute(AttributeName = "DT-VALIDADE-CNH-RENACH")]
//        public string DTVALIDADECNHRENACH { get; set; }
//        [XmlAttribute(AttributeName = "SEXO")]
//        public string SEXO { get; set; }
//        [XmlAttribute(AttributeName = "PRINC-CONDUTOR")]
//        public string PRINCCONDUTOR { get; set; }
//        [XmlAttribute(AttributeName = "CPF-CNPJ-CONDUTOR")]
//        public string CPFCNPJCONDUTOR { get; set; }
//    }

//    [XmlRoot(ElementName = "Apolice")]
//    public class Apolice
//    {
//        [XmlElement(ElementName = "Segurado")]
//        public Segurado Segurado { get; set; }
//        [XmlElement(ElementName = "Reclamante")]
//        public Reclamante Reclamante { get; set; }
//        [XmlElement(ElementName = "Condutor")]
//        public Condutor Condutor { get; set; }
//        [XmlAttribute(AttributeName = "COD-PROPOSTA")]
//        public string CODPROPOSTA { get; set; }
//        [XmlAttribute(AttributeName = "NUM-APOLICE")]
//        public string NUMAPOLICE { get; set; }
//        [XmlAttribute(AttributeName = "NUM-PROCESSO")]
//        public string NUMPROCESSO { get; set; }
//        [XmlAttribute(AttributeName = "COD-RECLAMANTE-SEGURADO")]
//        public string CODRECLAMANTESEGURADO { get; set; }
//        [XmlAttribute(AttributeName = "ESTADO-CIVIL-SEGURADO")]
//        public string ESTADOCIVILSEGURADO { get; set; }
//        [XmlAttribute(AttributeName = "SEGURADO-COM-FILHO-MENOS-16")]
//        public string SEGURADOCOMFILHOMENOS16 { get; set; }
//        [XmlAttribute(AttributeName = "SEGURADO-COM-FILHO-ENTRE-17-25")]
//        public string SEGURADOCOMFILHOENTRE1725 { get; set; }
//        [XmlAttribute(AttributeName = "RESIDENCIA-COM-MAIS-DE-UM-VEICULO")]
//        public string RESIDENCIACOMMAISDEUMVEICULO { get; set; }
//        [XmlAttribute(AttributeName = "SEGURADO-PROPRIETARIO-VEICULO")]
//        public string SEGURADOPROPRIETARIOVEICULO { get; set; }
//        [XmlAttribute(AttributeName = "DT-INICIO-VIGENCIA")]
//        public string DTINICIOVIGENCIA { get; set; }
//        [XmlAttribute(AttributeName = "DT-FIM-VIGENCIA")]
//        public string DTFIMVIGENCIA { get; set; }
//        [XmlAttribute(AttributeName = "TIPO-PESSOA")]
//        public string TIPOPESSOA { get; set; }
//        [XmlAttribute(AttributeName = "DT-NASC-PRINC-CONDUTOR")]
//        public string DTNASCPRINCCONDUTOR { get; set; }
//        [XmlAttribute(AttributeName = "CEP-PERNOITE")]
//        public string CEPPERNOITE { get; set; }
//        [XmlAttribute(AttributeName = "UTILIZA-CARRO-IR-TRABALHO")]
//        public string UTILIZACARROIRTRABALHO { get; set; }
//    }

//    [XmlRoot(ElementName = "EnderecoOficina")]
//    public class EnderecoOficina
//    {
//        [XmlAttribute(AttributeName = "LOGRAD-OFICINA")]
//        public string LOGRADOFICINA { get; set; }
//        [XmlAttribute(AttributeName = "BAIRRO-OFICINA")]
//        public string BAIRROOFICINA { get; set; }
//        [XmlAttribute(AttributeName = "CIDADE-OFICINA")]
//        public string CIDADEOFICINA { get; set; }
//        [XmlAttribute(AttributeName = "SIG-UF-OFICINA")]
//        public string SIGUFOFICINA { get; set; }
//    }

//    [XmlRoot(ElementName = "Oficina")]
//    public class Oficina
//    {
//        [XmlElement(ElementName = "EnderecoOficina")]
//        public EnderecoOficina EnderecoOficina { get; set; }
//        [XmlAttribute(AttributeName = "REFERENCIADA")]
//        public string REFERENCIADA { get; set; }
//        [XmlAttribute(AttributeName = "NM-OFICINA")]
//        public string NMOFICINA { get; set; }
//        [XmlAttribute(AttributeName = "TEL-COMERC-OFICINA")]
//        public string TELCOMERCOFICINA { get; set; }
//        [XmlAttribute(AttributeName = "CNPJ-OFICINA")]
//        public string CNPJOFICINA { get; set; }
//        [XmlAttribute(AttributeName = "VAL-PINTURA-ACABAMENTO")]
//        public string VALPINTURAACABAMENTO { get; set; }
//        [XmlAttribute(AttributeName = "VAL-MECANICA")]
//        public string VALMECANICA { get; set; }
//        [XmlAttribute(AttributeName = "VAL-LANTERN-CAPOTARIA")]
//        public string VALLANTERNCAPOTARIA { get; set; }
//    }

//    [XmlRoot(ElementName = "Veiculo")]
//    public class Veiculo
//    {
//        [XmlAttribute(AttributeName = "PLACA")]
//        public string PLACA { get; set; }
//        [XmlAttribute(AttributeName = "ANO-MODELO")]
//        public string ANOMODELO { get; set; }
//        [XmlAttribute(AttributeName = "DSC-COR")]
//        public string DSCCOR { get; set; }
//        [XmlAttribute(AttributeName = "COD-COMBUSTIVEL")]
//        public string CODCOMBUSTIVEL { get; set; }
//        [XmlAttribute(AttributeName = "SIG-CHASSI-SINISTRADO")]
//        public string SIGCHASSISINISTRADO { get; set; }
//        [XmlAttribute(AttributeName = "TIP-CARGA")]
//        public string TIPCARGA { get; set; }
//    }

//    [XmlRoot(ElementName = "Orcamento")]
//    public class Orcamento
//    {
//        [XmlAttribute(AttributeName = "VAL-FRANQUIA-LIQUIDA")]
//        public string VALFRANQUIALIQUIDA { get; set; }
//        [XmlAttribute(AttributeName = "VAL-FRANQUIA-APOLICE")]
//        public string VALFRANQUIAAPOLICE { get; set; }
//        [XmlAttribute(AttributeName = "VAL-DESCONTO-LIMITADO-A")]
//        public string VALDESCONTOLIMITADOA { get; set; }
//        [XmlAttribute(AttributeName = "VAL-COMERC-VEICULO")]
//        public string VALCOMERCVEICULO { get; set; }
//        [XmlAttribute(AttributeName = "PCT-FRANQUIA")]
//        public string PCTFRANQUIA { get; set; }
//        [XmlAttribute(AttributeName = "PCT-DESCONTO-FRANQUIA")]
//        public string PCTDESCONTOFRANQUIA { get; set; }
//        [XmlAttribute(AttributeName = "FLG-ISENCAO-FRANQUIA")]
//        public string FLGISENCAOFRANQUIA { get; set; }
//        [XmlAttribute(AttributeName = "DT-CHEGADA")]
//        public string DTCHEGADA { get; set; }
//    }

//    [XmlRoot(ElementName = "Cobertura")]
//    public class Cobertura
//    {
//        [XmlAttribute(AttributeName = "NUM-CLAUSULA")]
//        public string NUMCLAUSULA { get; set; }
//        [XmlAttribute(AttributeName = "COD-NATUREZA-COBERTURA")]
//        public string CODNATUREZACOBERTURA { get; set; }
//        [XmlAttribute(AttributeName = "VAL-FRANQUIA")]
//        public string VALFRANQUIA { get; set; }
//        [XmlAttribute(AttributeName = "TXT-NOTA-COBERTURA")]
//        public string TXTNOTACOBERTURA { get; set; }
//    }

//    [XmlRoot(ElementName = "OutrosDados")]
//    public class OutrosDados
//    {
//        [XmlElement(ElementName = "Cobertura")]
//        public Cobertura Cobertura { get; set; }
//        [XmlAttribute(AttributeName = "VAL-IS")]
//        public string VALIS { get; set; }
//        [XmlAttribute(AttributeName = "LMI-RCF-DM")]
//        public string LMIRCFDM { get; set; }
//        [XmlAttribute(AttributeName = "LMI-EQUIPAMENTO")]
//        public string LMIEQUIPAMENTO { get; set; }
//        [XmlAttribute(AttributeName = "LMI-CARROCERIA")]
//        public string LMICARROCERIA { get; set; }
//        [XmlAttribute(AttributeName = "LMI-BLINDAGEM")]
//        public string LMIBLINDAGEM { get; set; }
//        [XmlAttribute(AttributeName = "LMI-ACESSORIO")]
//        public string LMIACESSORIO { get; set; }
//        [XmlAttribute(AttributeName = "FLG-VEICULO-REBOCADO")]
//        public string FLGVEICULOREBOCADO { get; set; }
//        [XmlAttribute(AttributeName = "FLG-CONTRATO-FROTA")]
//        public string FLGCONTRATOFROTA { get; set; }
//        [XmlAttribute(AttributeName = "DSC-ACIDENTE")]
//        public string DSCACIDENTE { get; set; }
//        [XmlAttribute(AttributeName = "NUMERO-LOTE")]
//        public string NUMEROLOTE { get; set; }
//    }

//    [XmlRoot(ElementName = "CiaSeguros")]
//    public class CiaSeguros
//    {
//        [XmlAttribute(AttributeName = "COD-CIA")]
//        public string CODCIA { get; set; }
//        [XmlAttribute(AttributeName = "CGC-CIA")]
//        public string CGCCIA { get; set; }
//        [XmlAttribute(AttributeName = "NM-CIA-SEGUROS")]
//        public string NMCIASEGUROS { get; set; }
//        [XmlAttribute(AttributeName = "COD-SEGURADORA")]
//        public string CODSEGURADORA { get; set; }
//    }

//    [XmlRoot(ElementName = "DadosCompulsorios")]
//    public class DadosCompulsorios
//    {
//        [XmlElement(ElementName = "CiaSeguros")]
//        public CiaSeguros CiaSeguros { get; set; }
//        [XmlAttribute(AttributeName = "DT-TRANSMISSAO")]
//        public string DTTRANSMISSAO { get; set; }
//        [XmlAttribute(AttributeName = "TP-OPERACAO")]
//        public string TPOPERACAO { get; set; }
//        [XmlAttribute(AttributeName = "COD-SUCURSAL")]
//        public string CODSUCURSAL { get; set; }
//        [XmlAttribute(AttributeName = "COD-RAMO")]
//        public string CODRAMO { get; set; }
//        [XmlAttribute(AttributeName = "NUM-ITEM")]
//        public string NUMITEM { get; set; }
//        [XmlAttribute(AttributeName = "SUCURSAL-ATENDIMENTO")]
//        public string SUCURSALATENDIMENTO { get; set; }
//    }

//    [XmlRoot(ElementName = "DadosComplementares")]
//    public class DadosComplementares
//    {
//        [XmlAttribute(AttributeName = "PCT-DESCONTO-PECA")]
//        public string PCTDESCONTOPECA { get; set; }
//        [XmlAttribute(AttributeName = "PCT-DESCONTO-MO")]
//        public string PCTDESCONTOMO { get; set; }
//        [XmlAttribute(AttributeName = "FLG-ITEM-VIP")]
//        public string FLGITEMVIP { get; set; }
//        [XmlAttribute(AttributeName = "FLG-CAMINHAO-ESSENCIAL")]
//        public string FLGCAMINHAOESSENCIAL { get; set; }
//    }   
//}
