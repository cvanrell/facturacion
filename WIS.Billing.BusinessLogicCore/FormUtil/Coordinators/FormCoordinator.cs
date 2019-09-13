using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.Controllers;
using WIS.CommonCore.App;
using WIS.CommonCore.Exceptions;
using WIS.CommonCore.FormComponents;
using WIS.CommonCore.ServiceWrappers;

namespace WIS.BusinessLogicCore.FormUtil.Coordinators
{
    public class FormCoordinator : IFormCoordinator
    {
        private readonly IFormController _controller;
        private readonly IDbConnection _connection;

        public FormCoordinator(IFormController controller, IDbConnection connection)
        {
            this._controller = controller;
            this._connection = connection;
        }

        public IFormWrapper Initialize(IFormWrapper wrapper)
        {
            var data = wrapper.GetData<FormData>();

            IFormWrapper response = new FormWrapper(wrapper);

            try
            {
                data.Form = this._controller.FormInitialize(data.Form, data.Query, wrapper.User);

                response.SetData(data);
            }
            catch (WISException ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public IFormWrapper ValidateField(IFormWrapper wrapper)
        {
            var data = wrapper.GetData<FormValidationData>();

            IFormWrapper response = new FormWrapper(wrapper);

            try
            {
                data.Form = this._controller.FormValidateField(data.Form, data.Query, wrapper.User);

                response.SetData(data);
            }
            catch (WISException ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public IFormWrapper ButtonAction(IFormWrapper wrapper)
        {
            var data = wrapper.GetData<FormButtonActionData>();

            IFormWrapper response = new FormWrapper(wrapper);

            try
            {
                data.Form = this._controller.FormButtonAction(data.Form, data.Query, wrapper.User);

                response.SetData(data);
            }
            catch (WISException ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public IFormWrapper Submit(IFormWrapper wrapper)
        {
            var data = wrapper.GetData<FormSubmitData>();

            IFormWrapper response = new FormWrapper(wrapper);

            try
            {
                data.Form = this._controller.FormValidateForm(data.Form, data.Query, wrapper.User);

                if (data.Form.Fields.Any(d => !d.IsValid()))
                    throw new WISException("Se encontraron errores al validar");

                data.Form = this._controller.FormSubmit(data.Form, data.Query, wrapper.User);

                response.SetData(data);
            }
            catch (WISException ex)
            {
                response.SetData(data);
                response.SetError(ex.Message);
            }

            return response;
        }
        public IFormWrapper SelectSearch(IFormWrapper wrapper)
        {
            var data = wrapper.GetData<FormSelectSearchRequest>();

            IFormWrapper response = new FormWrapper(wrapper);

            try
            {
                List<SelectOption> options = this._controller.FormSelectSearch(data.Form, data.Query, wrapper.User);

                var responseData = new FormSelectSearchResponse
                {
                    Options = options
                };

                response.SetData(responseData);
            }
            catch (WISException ex)
            {
                response.SetData(data);
                response.SetError(ex.Message);
            }

            return response;
        }

        public IFormWrapper ExecuteAdjustments(IFormWrapper wrapper)
        {
            var data = wrapper.GetData<FormButtonActionData>();

            IFormWrapper response = new FormWrapper(wrapper);

            try
            {
                data.Form = this._controller.ExecuteAdjustments(data.Form, data.Query, wrapper.User);

                response.SetData(data);
            }
            catch (WISException ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
    }
}
