using System.Collections.Generic;
using System.Xml.Serialization;

namespace audatex.br.audabridge2.domain.model.i360.output
{
    [XmlRoot(ElementName = "Output")]
    public class OutputModel
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
        public string NumeroCaso { get; set; }
        public string NumeroExpediente { get; set; }
        public string Placas { get; set; }
        public string VIN { get; set; }
        public string VIN2 { get; set; }
        public string Evento { get; set; }
        public string Transaccion { get; set; }
        public string WAN { get; set; }
        public string IDExpediente { get; set; }
        public string IDAttachment { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
    }

    public class Body
    {
        public BusquedaExpediente BusquedaExpediente { get; set; }
        public DetalleExpediente DetalleExpediente { get; set; }
        public ListadoAttachment ListadoAttachment { get; set; }
        public DetalleAttachment DetalleAttachment { get; set; }
        public DetalleInpart DetalleInpart { get; set; }
        public ListadoEventos ListadoEventos { get; set; }
        public Valuacion Valuacion { get; set; }
        public EstatusInpart EstatusInpart { get; set; }
        public ResumenProveedorInpart ResumenProveedorInpart { get; set; }
        public ConsultaTalleres ConsultaTalleres { get; set; }
        public Notificaciones Notificaciones { get; set; }
        public Salvamento Salvamento { get; set; }
        public Reporte Reporte { get; set; }
    }

    public class BusquedaExpediente
    {
        public Expedientes Expedientes { get; set; }
    }

    public class Expedientes
    {
        public List<Expediente> Expediente { get; set; }
    }

    public class Expediente
    {
        public string IDExpediente { get; set; }
        public string NumeroCaso { get; set; }
        public string NumeroExpediente { get; set; }
        public string ReferenciaInterna { get; set; }
        public string FechaCreacion { get; set; }
        public string Placas { get; set; }
        public string VIN { get; set; }
        public string VIN2 { get; set; }
        public string WAN { get; set; }
        public Eventos Eventos { get; set; }
    }

    public class DetalleExpediente
    {
        public DatosGenerales DatosGenerales { get; set; }
        public Direcciones Direcciones { get; set; }
        public DatosVehiculo DatosVehiculo { get; set; }
        public OpcionesCalculo OpcionesCalculo { get; set; }
        public OperacionesPintura OperacionesPintura { get; set; }
        public OperacionesManoObra OperacionesManoObra { get; set; }
        public Piezas Piezas { get; set; }
        public CodigosOpcionales CodigosOpcionales { get; set; }
        public CodigosEquipo CodigosEquipo { get; set; }
        public TotalesPintura TotalesPintura { get; set; }
        public Totales Totales { get; set; }
        public AudaVales AudaVales { get; set; }
        public Textos Textos { get; set; }
        public CamposDinamicos CamposDinamicos { get; set; }
    }

    public class DatosGenerales
    {
        public string IDExpediente { get; set; }
        public string NumeroCaso { get; set; }
        public string NumeroExpediente { get; set; }
        public string WAN { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaUltimaActualizacion { get; set; }
        public string FechaAccidente { get; set; }
        public string FechaIngresoTaller { get; set; }
        public string FechaEvaluacion { get; set; }
        public string FechaEnvioAutorizacion { get; set; }
        public string FechaAutorizacion { get; set; }
        public string EstatusAutorizacion { get; set; }
        public string AutorizadoPor { get; set; }
        public string CodigoUsuarioAutorizacion { get; set; }
        public string FechaRechazo { get; set; }
        public string FechaPromesaEntrega { get; set; }
        public string Admin1 { get; set; }
        public string Admin2 { get; set; }
        public string Admin3 { get; set; }
        public string Admin4 { get; set; }
        public string Poliza { get; set; }
        public string Endoso { get; set; }
        public string NombreAsegurado { get; set; }
        public string ApellidoAsegurado { get; set; }
        public string DescripcionAccidente { get; set; }
        public string NumeroSerial { get; set; }
        public string AdministradorExpediente { get; set; }
        public string TelefonoContacto { get; set; }
        public string AnioFabricacion { get; set; }
        public string FechaAsignacion { get; set; }
        public string FechaPrimerContacto { get; set; }
        public string TipoCaso { get; set; }
        public string ValorComercial { get; set; }
        public string Deducible { get; set; }
        public string DeduciblePorcentaje { get; set; }
        public string DeducibleMinimo { get; set; }
        public string Odometro { get; set; }
        public string Referencia { get; set; }
        public string CantidadAsegurada { get; set; }
        public string ValorAdquisicionVehiculo { get; set; }
        public string Contrario { get; set; }
        public string Taller { get; set; }
        public string CondicionesGenerales { get; set; }
        public string Localidad { get; set; }
        public string FechaExpiracionPoliza { get; set; }
        public string FechaEmisionPoliza { get; set; }
        public string CreadoPor { get; set; }
        public string FechaInspeccion { get; set; }
        public string TipoExpediente { get; set; }
        public string CausaAccidente { get; set; }
        public string ValorActualCompra { get; set; }
        public string ValorSalvamento { get; set; }
        public string NumeroMotorVehiculo { get; set; }
        public string AnioPolizaVehiculo { get; set; }
        public string TipoMotorVehiculo { get; set; }
        public string CajaVelocidadesVehiculo { get; set; }
        public string FechaReparacionAsignada { get; set; }
        public string FechaReparacionCompletada { get; set; }
        public string CostoEstimadoReparacion { get; set; }
        public string Registro { get; set; }
        public string SegundoApellidoAsegurado { get; set; }
        public string FechaInicioReparacion { get; set; }
        public string CodigoTaller { get; set; }
        public string CoberturaGarantia { get; set; }
        public string CodigoFasecolda { get; set; }
        public string DanosPrevios { get; set; }
        public string Motivo { get; set; }
        public string Justificativa { get; set; }
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

    public class DatosVehiculo
    {
        public string CodigoFabricante { get; set; }
        public string CodigoVersion { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public string Version { get; set; }
        public string CodigosEquipo { get; set; }
        public string TipoVehiculo { get; set; }
        public string NumeroPuertas { get; set; }
        public string CodigoPintura { get; set; }
        public string ColorVehiculo { get; set; }
    }

    public class OpcionesCalculo
    {
        public string UTProteccionAnticorrosion1 { get; set; }
        public string UTProteccionAnticorrosion2 { get; set; }
        public string UTManoObra1 { get; set; }
        public string UTManoObra2 { get; set; }
        public string UTManoObra3 { get; set; }
        public string UTManoObra4 { get; set; }
        public string UTManoObra5 { get; set; }
        public string UTAlineacionVisual { get; set; }
        public string TiempoBaseNoPintura { get; set; }
        public string TiempoBasePintura { get; set; }
        public string TipoPintura { get; set; }
        public string FechaListaPrecios { get; set; }
        public string FechaCalculo { get; set; }
        public string TarifaTiempoBasePintura { get; set; }
        public string TarifaMecanica { get; set; }
        public string TarifaPintura { get; set; }
        public string TarifaCarroceria { get; set; }
        public string TarifaManoObra2 { get; set; }
        public string TarifaManoObra3 { get; set; }
        public string TarifaManoObra4 { get; set; }
        public string TarifaManoObra5 { get; set; }
        public string TarifaManoObra6 { get; set; }
    }

    public class OperacionesPintura
    {
        public List<OperacionPintura> OperacionPintura { get; set; }
    }

    public class OperacionPintura
    {
        public string Posicion { get; set; }
        public string Descripcion { get; set; }
        public string UT { get; set; }
        public string Monto { get; set; }
    }

    public class OperacionesManoObra
    {
        public List<OperacionManoObra> OperacionManoObra { get; set; }
    }

    public class OperacionManoObra
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string UT { get; set; }
        public string Monto { get; set; }
    }

    public class Piezas
    {
        public List<Pieza> Pieza { get; set; }
    }

    public class Pieza
    {
        public string Posicion { get; set; }
        public string NumeroParte { get; set; }
        public string Descripcion { get; set; }
        public string Monto { get; set; }
        public string CambioPrecio { get; set; }
        public string InfoAdicional { get; set; }
        public string EstatusInpart { get; set; }
        public string EnviadoInpart { get; set; }
        public string IDPieza { get; set; }
        public string Consecutivo { get; set; }
        public string SerialNumeroParte { get; set; }
        public string FechaEntrega { get; set; }
        public string NombreEntrega { get; set; }
        public string FechaEstimadaLlegada { get; set; }
        public string NumeroFactura { get; set; }
        public string Nueva { get; set; }
        public string Precio { get; set; }
        public string Cantidad { get; set; }
        public string FechaRecepcion { get; set; }
        public string NombreRecepcion { get; set; }
        public string SGN { get; set; }
        public string Estatus { get; set; }
        public string Tipo { get; set; }
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

    public class CodigosEquipo
    {
        public List<CodigoEquipo> CodigoEquipo { get; set; }
    }

    public class CodigoEquipo
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class TotalesPintura
    {
        public string MontoFijoManoObraPintura { get; set; }
        public string MontoConstanteMaterialMetal { get; set; }
        public string MontoConstanteMaterialPlastico { get; set; }
        public string MontoMaterialesPintura { get; set; }
        public string UTPintura { get; set; }
        public string UTManoObraPintura { get; set; }
        public string UTPartes { get; set; }
        public string MontoManoObraPintura { get; set; }
        public string MontoTotalManoObraPintura { get; set; }
        public string MontoTotalPintura { get; set; }
        public string TotalUTPintura { get; set; }
    }

    public class Totales
    {
        public string MontoProteccionAnticorrosion1 { get; set; }
        public string MontoProteccionAnticorrosion2 { get; set; }
        public string MontoDescuentoDespuesIVA { get; set; }
        public string PorcentajeDescuentoDespuesIVA { get; set; }
        public string MontoDescuentoAntesIVA { get; set; }
        public string PorcentajeDescuentoAntesIVA { get; set; }
        public string MontoDescuentoPrepago { get; set; }
        public string PorcentajeDescuentoPrepago { get; set; }
        public string MontoDescuentoPostpago { get; set; }
        public string PorcentajeDescuentoPostpago { get; set; }
        public string CostoIntermedioReparacion { get; set; }
        public string MaterialIRE { get; set; }
        public string MontoManoObra2 { get; set; }
        public string MontoManoObra3 { get; set; }
        public string MontoManoObra4 { get; set; }
        public string MontoManoObra5 { get; set; }
        public string MontoManoObra6 { get; set; }
        public string MontoManoObraPanelMecanica { get; set; }
        public string UTManoObra1 { get; set; }
        public string UTManoObra2 { get; set; }
        public string UTManoObra3 { get; set; }
        public string UTManoObra4 { get; set; }
        public string UTManoObra5 { get; set; }
        public string UTManoObra6 { get; set; }
        public string MontoDecrementoManoObra { get; set; }
        public string MontoIncrementoManoObra { get; set; }
        public string MontoPartes { get; set; }
        public string AhorroPartes { get; set; }
        public string MontoCostoReparacion { get; set; }
        public string CostoReparacionConIVAAntesDeducciones { get; set; }
        public string MontoTotalAdicional { get; set; }
        public string MontoTotalExtra { get; set; }
        public string MontoTotalManoObra { get; set; }
        public string MontoTotalPartes { get; set; }
        public string MontoIVAAntesDeducciones { get; set; }
        public string MontoAlineacionVisual { get; set; }
        public string MontoIVA { get; set; }
        public string PorcentajeIVA { get; set; }
        public string GranTotalSinIVA { get; set; }
        public string GranTotalConIVA { get; set; }
    }

    public class AudaVales
    {
        public List<AudaVale> AudaVale { get; set; }
    }

    public class AudaVale
    {
        public string Folio { get; set; }
        public string FechaExpedicion { get; set; }
        public string FechaImpresion { get; set; }
        public string IDUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public PiezasAudaVale PiezasAudaVale { get; set; }
        public string Observaciones { get; set; }
        public string ClaveProveedor { get; set; }
        public string Proveedor { get; set; }
        public string Estatus { get; set; }
    }

    public class PiezasAudaVale
    {
        public List<PiezaAudaVale> PiezaAudaVale { get; set; }
    }

    public class PiezaAudaVale
    {
        public string Posicion { get; set; }
        public string NumeroParte { get; set; }
        public string Descripcion { get; set; }
        public string TipoPieza { get; set; }
        public string Importe { get; set; }
        public string Total { get; set; }
    }

    public class Textos
    {
        public List<Texto> Texto { get; set; }
    }

    public class Texto
    {
        public string Autor { get; set; }
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

    public class ListadoAttachment
    {
        public Imagenes Imagenes { get; set; }
        public Attachments Attachments { get; set; }
    }

    public class Imagenes
    {
        public List<Imagen> Imagen { get; set; }
    }

    public class Imagen
    {
        public string IDImagen { get; set; }
        public string TipoAttachment { get; set; }
        public string Descripcion { get; set; }
        public string FechaCreacion { get; set; }
        public string Comentarios { get; set; }
        public string URL { get; set; }
        public string Descargado { get; set; }
    }

    public class Attachments
    {
        public List<Attachment> Attachment { get; set; }
    }

    public class Attachment
    {
        public string IDAttachment { get; set; }
        public string TipoAttachment { get; set; }
        public string FechaCreacion { get; set; }
        public string NombreArchivo { get; set; }
        public string Tamano { get; set; }
        public string Descargado { get; set; }
    }

    public class DetalleAttachment
    {
        public string IDAttachment { get; set; }
        public string FechaCreacion { get; set; }
        public string NumeroSecuencia { get; set; }
        public string Comentarios { get; set; }
        public string Tamano { get; set; }
        public string NombreArchivo { get; set; }
        public string Extension { get; set; }
        public string Tipo { get; set; }
        public string Subtipo { get; set; }
        public string Contenido { get; set; }
    }

    public class DetalleInpart
    {
        public Proveedores Proveedores { get; set; }
    }

    public class Proveedores
    {
        public List<Proveedor> Proveedor { get; set; }
    }

    public class Proveedor
    {
        public string IDProveedor { get; set; }
        public string CodigoProveedor { get; set; }
        public string TipoProveedor { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public Piezas Piezas { get; set; }
    }

    public class ListadoEventos
    {
        public Expedientes Expedientes { get; set; }
    }

    public class Eventos
    {
        public Evento Evento { get; set; }
    }

    public class Evento
    {
        public string IDEvento { get; set; }
        public string EventoGeneradoPor { get; set; }
        public string FechaEvento { get; set; }
        public string CodigoEvento { get; set; }
        public string DescripcionEvento { get; set; }
        public string TipoEvento { get; set; }
    }

    public class Valuacion
    {
        public string Contenido { get; set; }
        public string FechaCreacion { get; set; }
    }

    public class EstatusInpart
    {
        public EstatusProveedores EstatusProveedores { get; set; }
    }

    public class EstatusProveedores
    {
        public List<EstatusProveedor> EstatusProveedor { get; set; }
    }

    public class EstatusProveedor
    {
        public string CodigoProveedor { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public EstatusPiezas EstatusPiezas { get; set; }
    }

    public class EstatusPiezas
    {
        public List<EstatusPieza> EstatusPieza { get; set; }
    }

    public class EstatusPieza
    {
        public string Consecutivo { get; set; }
        public string NumeroParte { get; set; }
        public string SerialNumeroParte { get; set; }
        public string Descripcion { get; set; }
        public string NumeroFactura { get; set; }
        public string Precio { get; set; }
        public string SGN { get; set; }
        public string Estatus { get; set; }
    }

    public class ResumenProveedorInpart
    {
        public ResumenProveedores ResumenProveedores { get; set; }
    }

    public class ResumenProveedores
    {
        public List<ResumenProveedor> ResumenProveedor { get; set; }
    }

    public class ResumenProveedor
    {
        public string CodigoProveedor { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public ResumenTotales ResumenTotales { get; set; }
    }

    public class ResumenTotales
    {
        public List<ResumenTotal> ResumenTotal { get; set; }
    }

    public class ResumenTotal
    {
        public string EstatusResumen { get; set; }
        public string MontoResumen { get; set; }
    }

    public class ConsultaTalleres
    {
        public Talleres Talleres { get; set; }
    }

    public class Talleres
    {
        public List<Taller> Taller { get; set; }
    }

    public class Taller
    {
        public string IDTaller { get; set; }
        public string NombreTaller { get; set; }
        public string CodigoIntegracion { get; set; }
        public string CodigoProveedor { get; set; }
        public Pools Pools { get; set; }
    }

    public class Pools
    {
        public List<Pool> Pool { get; set; }
    }

    public class Pool
    {
        public string IDPool { get; set; }
        public string NombrePool { get; set; }
        public Usuarios Usuarios { get; set; }
    }

    public class Usuarios
    {
        public List<Usuario> Usuario { get; set; }
    }

    public class Usuario
    {
        public string Login { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Descripcion { get; set; }
        public string Email { get; set; }
    }

    public class Notificaciones
    {
        public List<Notificacion> Notificacion { get; set; }
    }

    public class Notificacion
    {
        public string Compania { get; set; }
        public string DescripcionCompania { get; set; }
        public string NumeroExpediente { get; set; }
        public string WAN { get; set; }
        public string Fecha { get; set; }
        public string Mensaje { get; set; }
    }

    public class Salvamento
    {
        public string ValorComercial { get; set; }
        public string MontoEstimadoReparacion { get; set; }
        public string NotasRevision { get; set; }
        public string Notas { get; set; }
        public string Reparable { get; set; }
        public string ValorMercadoVehiculo { get; set; }
        public CheckList CheckList { get; set; }
    }

    public class CheckList
    {
        public List<CheckListItem> CheckListItem { get; set; }
    }

    public class CheckListItem
    {
        public string DescripcionItem { get; set; }
        public string Notas { get; set; }
        public string OpcionPiezaVehiculo { get; set; }
        public string CondicionPieza { get; set; }
        public string DescripcionCondicionPieza { get; set; }
        public string Activo { get; set; }
        public string CodigoConfiguracion { get; set; }
        public string DescripcionConfiguracion { get; set; }
        public string Categoria { get; set; }
        public string Tipo { get; set; }
    }

    public class Reporte
    {
        public string Tipo { get; set; }
        public string FechaCreacion { get; set; }
        public string NombreArchivo { get; set; }
        public string Comentarios { get; set; }
        public string Contenido { get; set; }
    }
}
