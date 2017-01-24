
using System.Collections.Generic;
using System.Xml.Serialization;

namespace audatex.br.audabridge2.domain.model.i360.input
{
    [XmlRoot(ElementName = "Input")]
    public class InputModel
    {
        public ObRequest ObRequest { get; set; }
    }

    public class ObRequest
    {
        public Header Header { get; set; }
        public Body Body { get; set; }
    }

    public class Header
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Operacion { get; set; }
        public string WAN { get; set; }
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
    }

    public class Body
    {
        public string NumeroExpediente { get; set; }
        public string NumeroCaso { get; set; }
        public string Poliza { get; set; }
        public string Endoso { get; set; }
        public string NombreAsegurado { get; set; }
        public string ApellidoAsegurado { get; set; }
        public string FechaAccidente { get; set; }
        public string DescripcionAccidente { get; set; }
        public string Placas { get; set; }
        public string Fabricante { get; set; }
        public string NumeroSerial { get; set; }
        public string AdministradorExpediente { get; set; }
        public string TelefonoContacto { get; set; }
        public string AnioFabricacion { get; set; }
        public string FechaAsignacion { get; set; }
        public string FechaPromesaEntrega { get; set; }
        public string FechaPrimerContacto { get; set; }
        public string TipoCaso { get; set; }
        public string ValorComercial { get; set; }
        public string Deducible { get; set; }
        public string DeduciblePorcentaje { get; set; }
        public string DeducibleMinimo { get; set; }
        public string VIN { get; set; }
        public CodigosOpcionales CodigosOpcionales { get; set; }
        public string TarifaCarroceria { get; set; }
        public string TarifaPintura { get; set; }
        public string TarifaMecanica { get; set; }
        public string TarifaTiempoBasePintura { get; set; }
        public string Odometro { get; set; }
        public string Referencia { get; set; }
        public string CantidadAsegurada { get; set; }
        public string ValorAdquisicionVehiculo { get; set; }
        public string CompaniaOrigen { get; set; }
        public string CompaniaAsignada { get; set; }
        public string CodigoCompaniaAsignada { get; set; }
        public string UsuarioAsignado { get; set; }
        public string CodigoUsuarioAsignado { get; set; }
        public string PoolAsignado { get; set; }
        public string Contrario { get; set; }
        public string Taller { get; set; }
        public string CondicionesGenerales { get; set; }
        public string DatosAdministrativos1 { get; set; }
        public string DatosAdministrativos2 { get; set; }
        public string DatosAdministrativos3 { get; set; }
        public string DatosAdministrativos4 { get; set; }
        public string Localidad { get; set; }
        public string FechaExpiracionPoliza { get; set; }
        public string FechaEmisionPoliza { get; set; }
        public string CreadoPor { get; set; }
        public Direcciones Direcciones { get; set; }
        public string FechaInspeccion { get; set; }
        public string TipoExpediente { get; set; }
        public string CausaAccidente { get; set; }
        public string ReferenciaInterna { get; set; }
        public string ValorActualCompra { get; set; }
        public string ValorSalvamento { get; set; }
        public string NumeroMotorVehiculo { get; set; }
        public string AnioPolizaVehiculo { get; set; }
        public string TipoMotorVehiculo { get; set; }
        public string CajaVelocidadesVehiculo { get; set; }
        public string FechaReparacionAsignada { get; set; }
        public string FechaCreacion { get; set; }
        public string WAN { get; set; }
        public string FechaReparacionCompletada { get; set; }
        public string CostoEstimadoReparacion { get; set; }
        public string Registro { get; set; }
        public string SegundoApellidoAsegurado { get; set; }
        public string FechaInicioReparacion { get; set; }
        public string CoberturaGarantia { get; set; }
        public string CodigoFasecolda { get; set; }
        public Imagen Imagen { get; set; }
        public string TipoVehiculo { get; set; }
        public string NumeroPuertas { get; set; }
        public string CodigoPintura { get; set; }
        public string ColorVehiculo { get; set; }
        public Textos Textos { get; set; }
        public CamposDinamicos CamposDinamicos { get; set; }
    }

    public class CodigosOpcionales
    {
        public List<CodigoOpcional> CodigoOpcional { get; set; }
    }

    public class CodigoOpcional
    {
        public string Codigo { get; set; }
        public string Valor { get; set; }
    }

    public class Direcciones
    {
        public List<Direccion> Direccion { get; set; }
    }

    public class Direccion
    {
        public string Titulo { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Linea1 { get; set; }
        public string Linea2 { get; set; }
        public string Linea3 { get; set; }
        public string Linea4 { get; set; }
        public string EMail1 { get; set; }
        public string EMail2 { get; set; }
        public string CodigoPostal { get; set; }
        public string TelefonoHogar { get; set; }
        public string Fax { get; set; }
        public string TelefonoOficina { get; set; }
        public string TelefonoMovil { get; set; }
        public string TelefonoOtro { get; set; }
        public string CertificadoLegal { get; set; }
        public string Comentarios { get; set; }
        public string TipoContacto { get; set; }
        public string CodigoIntegracion { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
    }

    public class Imagen
    {
        public string NombreArchivo { get; set; }
        public string Comentario { get; set; }
        public string Tamano { get; set; }
        public string Contenido { get; set; }
    }

    public class Textos
    {
        public List<Texto> Texto { get; set; }
    }

    public class Texto
    {
        public string Contenido { get; set; }
        public string TipoTexto { get; set; }
    }

    public class CamposDinamicos
    {
        public List<CampoDinamico> CampoDinamico { get; set; }
    }

    public class CampoDinamico
    {
        public string Secuencia { get; set; }
        public string Padre { get; set; }
        public string Nivel { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
    }
}