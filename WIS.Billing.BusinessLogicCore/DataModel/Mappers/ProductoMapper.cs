using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogic.WMS;
using WIS.Persistance.Database;

namespace WIS.BusinessLogic.DataModel.Mappers
{
    public class ProductoMapper : Mapper
    {
        public ProductoMapper()
        {
        }

        public Producto MapToObject(T_PRODUTO productoEntity)
        {
            return new Producto
            {
                Codigo = productoEntity.CD_PRODUTO,
                Empresa = productoEntity.CD_EMPRESA,
                CodigoMercadologico = productoEntity.CD_MERCADOLOGICO,
                CodigoProductoEmpresa = productoEntity.CD_PRODUTO_EMPRESA,
                ManejaIdentificador = this.MapStringToBoolean(productoEntity.ID_MANEJO_IDENTIFICADOR),
                NAM = productoEntity.CD_NAM
            };
        }

        public T_PRODUTO MapToEntity(Producto producto)
        {
            return new T_PRODUTO
            {
                CD_PRODUTO = producto.Codigo,
                CD_EMPRESA = producto.Empresa,
                CD_MERCADOLOGICO = producto.CodigoMercadologico,
                CD_PRODUTO_EMPRESA = producto.CodigoProductoEmpresa,
                ID_MANEJO_IDENTIFICADOR = this.MapBoolToString(producto.ManejaIdentificador),
                CD_NAM = producto.NAM
            };
        }
    }
}
