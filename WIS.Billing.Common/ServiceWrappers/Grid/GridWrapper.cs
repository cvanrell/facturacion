using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using WIS.Common.Enums;
using WIS.Common.Serialization.Binders;

namespace WIS.Common.ServiceWrappers
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
                typeof(WIS.Common.GridComponents.Grid),
                typeof(WIS.Common.GridComponents.GridRow),
                typeof(WIS.Common.GridComponents.GridColumn),
                typeof(WIS.Common.GridComponents.GridColumnButton),
                typeof(WIS.Common.GridComponents.GridColumnItemList),
                typeof(WIS.Common.GridComponents.GridColumnText),
                typeof(WIS.Common.GridComponents.GridCell),
                
                typeof(WIS.Common.GridComponents.GridItem),
                typeof(WIS.Common.GridComponents.GridItemHeader),
                typeof(WIS.Common.GridComponents.GridItemDivider),               
                
                typeof(WIS.Common.GridComponents.GridButton),
                typeof(WIS.Common.GridComponents.GridSelection),

                typeof(WIS.Common.GridComponents.GridCommitRequest),
                typeof(WIS.Common.GridComponents.GridInitializeResponse),
                typeof(WIS.Common.GridComponents.GridFetchRequest),
                typeof(WIS.Common.GridComponents.GridFetchResponse),                
                typeof(WIS.Common.GridComponents.GridValidationRequest),
                typeof(WIS.Common.GridComponents.GridValidationResponse),
                typeof(WIS.Common.GridComponents.GridButtonAction),
                typeof(WIS.Common.GridComponents.GridMenuItemAction),

                typeof(WIS.Common.FilterComponents.FilterCommand),
                typeof(WIS.Common.SortComponents.SortCommand),

                typeof(WIS.Common.App.ComponentParameter)
            });
        }
    }
}
