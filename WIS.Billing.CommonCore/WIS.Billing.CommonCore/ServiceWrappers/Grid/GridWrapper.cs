using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using WIS.CommonCore.Enums;
using WIS.CommonCore.Serialization.Binders;

namespace WIS.CommonCore.ServiceWrappers
{
    public class GridWrapper : TransferWrapper, ITransferWrapper, IGridWrapper
    {
        public GridAction Action { get; set; }
        public string GridId { get; set; }

        public GridWrapper() : base()
        {
        }
        public GridWrapper(IGridWrapper wrapper) : base(wrapper)
        {
            this.GridId = wrapper.GridId;
        }

        public override ISerializationBinder GetSerializationBinder()
        {
            //Esto se define por seguridad, no se permite pasar tipos no esperados
            return new CustomSerializationBinder(new List<Type> {
                typeof(WIS.CommonCore.GridComponents.Grid),
                typeof(WIS.CommonCore.GridComponents.GridRow),
                typeof(WIS.CommonCore.GridComponents.GridColumn),
                typeof(WIS.CommonCore.GridComponents.GridColumnButton),
                typeof(WIS.CommonCore.GridComponents.GridColumnItemList),
                typeof(WIS.CommonCore.GridComponents.GridColumnText),
                typeof(WIS.CommonCore.GridComponents.GridCell),
                
                typeof(WIS.CommonCore.GridComponents.GridItem),
                typeof(WIS.CommonCore.GridComponents.GridItemHeader),
                typeof(WIS.CommonCore.GridComponents.GridItemDivider),               
                
                typeof(WIS.CommonCore.GridComponents.GridButton),
                typeof(WIS.CommonCore.GridComponents.GridSelection),

                typeof(WIS.CommonCore.GridComponents.GridCommitRequest),
                typeof(WIS.CommonCore.GridComponents.GridInitializeResponse),
                typeof(WIS.CommonCore.GridComponents.GridFetchRequest),
                typeof(WIS.CommonCore.GridComponents.GridFetchResponse),                
                typeof(WIS.CommonCore.GridComponents.GridValidationRequest),
                typeof(WIS.CommonCore.GridComponents.GridValidationResponse),
                typeof(WIS.CommonCore.GridComponents.GridButtonAction),
                typeof(WIS.CommonCore.GridComponents.GridMenuItemAction),

                typeof(WIS.CommonCore.FilterComponents.FilterCommand),
                typeof(WIS.CommonCore.SortComponents.SortCommand),

                typeof(WIS.CommonCore.App.ComponentParameter)
            });
        }
    }
}
