"use strict";function e(e){return e&&"object"==typeof e&&"default"in e?e.default:e}Object.defineProperty(exports,"__esModule",{value:!0});var t,n=require("tslib"),r=require("react"),i=e(require("react-fast-compare")),o=e(require("deepmerge")),a=e(require("hoist-non-react-statics")),s=e(require("create-react-context")),u=e(require("tiny-warning")),l=e(require("lodash/clone")),d=e(require("lodash/toPath")),p=e(require("lodash/cloneDeep")),c=(t=s({})).Provider,h=t.Consumer;function m(e){var t=function(t){return r.createElement(h,null,function(i){return r.createElement(e,n.__assign({},t,{formik:i}))})},i=e.displayName||e.name||e.constructor&&e.constructor.name||"Component";return t.WrappedComponent=e,t.displayName="FormikConnect("+i+")",a(t,e)}var f=function(e){return"function"==typeof e},v=function(e){return null!==e&&"object"==typeof e},F=function(e){return String(Math.floor(Number(e)))===e},g=function(e){return"[object String]"===Object.prototype.toString.call(e)},b=function(e){return e!=e},S=function(e){return 0===r.Children.count(e)},y=function(e){return v(e)&&f(e.then)},_=function(e){return e&&v(e)&&v(e.target)};function k(e){var t=!1;return[new Promise(function(n,r){e.then(function(e){return t?r({isCanceled:!0}):n(e)},function(e){return r(t?{isCanceled:!0}:e)})}),function(){t=!0}]}function V(e,t,n,r){void 0===r&&(r=0);for(var i=d(t);e&&r<i.length;)e=e[i[r++]];return void 0===e?n:e}function C(e,t,n){for(var r=l(e),i=r,o=0,a=d(t);o<a.length-1;o++){var s=a[o],u=V(e,a.slice(0,o+1));if(u)i=i[s]=l(u);else{var p=a[o+1];i=i[s]=F(p)&&Number(p)>=0?[]:{}}}return(0===o?e:i)[a[o]]===n?e:(void 0===n?delete i[a[o]]:i[a[o]]=n,0===o&&void 0===n&&delete r[a[o]],r)}function x(e,t,n,r){void 0===n&&(n=new WeakMap),void 0===r&&(r={});for(var i=0,o=Object.keys(e);i<o.length;i++){var a=o[i],s=e[a];v(s)?n.get(s)||(n.set(s,!0),r[a]=Array.isArray(s)?[]:{},x(s,t,n,r[a])):r[a]=t}return r}var O=function(e){function t(t){var r=e.call(this,t)||this;return r.hcCache={},r.hbCache={},r.registerField=function(e,t){r.fields[e]=t},r.unregisterField=function(e){delete r.fields[e]},r.setErrors=function(e){r.setState({errors:e})},r.setTouched=function(e){r.setState({touched:e},function(){r.props.validateOnBlur&&r.runValidations(r.state.values)})},r.setValues=function(e){r.setState({values:e},function(){r.props.validateOnChange&&r.runValidations(e)})},r.setStatus=function(e){r.setState({status:e})},r.setError=function(e){r.setState({error:e})},r.setSubmitting=function(e){r.didMount&&r.setState({isSubmitting:e})},r.validateField=function(e){return r.setState({isValidating:!0}),r.runSingleFieldLevelValidation(e,V(r.state.values,e)).then(function(t){return r.didMount&&r.setState({errors:C(r.state.errors,e,t),isValidating:!1}),t})},r.runSingleFieldLevelValidation=function(e,t){return new Promise(function(n){return n(r.fields[e].props.validate(t))}).then(function(e){return e},function(e){return e})},r.runValidationSchema=function(e){return new Promise(function(t){var n=r.props.validationSchema,i=f(n)?n():n;P(e,i).then(function(){t({})},function(e){t(E(e))})})},r.runValidations=function(e,t){void 0===e&&(e=r.state.values),r.validator&&r.validator();var n=k(Promise.all([r.runFieldLevelValidations(e,t),r.props.validationSchema?r.runValidationSchema(e):{},r.props.validate?r.runValidateHandler(e):{}]).then(function(e){return o.all([e[0],e[1],e[2]],{arrayMerge:w})})),a=n[0];return r.validator=n[1],a.then(function(e){return r.didMount&&r.setState(function(n){return i(n.errors,e)?null:t&&!Object.prototype.hasOwnProperty.call(e,t)?{errors:Object.keys(n.errors).reduce(function(e,r){return r!==t&&(e[r]=n.errors[r]),e},{})}:{errors:Object.assign({},n.errors,e)}}),e}).catch(function(e){return e})},r.handleChange=function(e){var t=function(e,t){var i,o=t;if(_(e)){var a=e;a.persist&&a.persist();var s=a.target,u=s.type,l=s.name,d=s.checked;if(o=t||(l||s.id),i=a.target.value,/number|range/.test(u)){var p=parseFloat(a.target.value);i=b(p)?"":p}/checkbox/.test(u)&&(i=d)}else i=e;o&&r.setState(function(e){return n.__assign({},e,{values:C(e.values,o,i)})},function(){r.props.validateOnChange&&r.runValidations(C(r.state.values,o,i),o)})};if(g(e)){var i=e;return f(r.hcCache[i])||(r.hcCache[i]=function(e){return t(e,i)}),r.hcCache[i]}t(e)},r.setFieldValue=function(e,t,i){void 0===i&&(i=!0),r.didMount&&r.setState(function(r){return n.__assign({},r,{values:C(r.values,e,t)})},function(){r.props.validateOnChange&&i&&r.runValidations(r.state.values,e)})},r.handleSubmit=function(e){e&&e.preventDefault&&e.preventDefault(),r.submitForm()},r.submitForm=function(){r.props.validateOnSubmit?r.submitFormWithValidation():r.submitFormWithoutValidation()},r.submitFormWithoutValidation=function(){return r.setState(function(e){return{isSubmitting:!0,submitCount:e.submitCount+1}}),new Promise(function(e){r.executeSubmit(),e()})},r.submitFormWithValidation=function(){return r.setState(function(e){return{touched:x(e.values,!0),isSubmitting:!0,isValidating:!0,submitCount:e.submitCount+1}}),r.runValidations(r.state.values).then(function(e){r.didMount&&r.setState({isValidating:!1}),0===Object.keys(e).length?r.executeSubmit():r.didMount&&r.setState({isSubmitting:!1})})},r.executeSubmit=function(){r.props.onSubmit(r.state.values,r.getFormikActions())},r.handleBlur=function(e){var t=function(e,t){var n=t;if(_(e)){var i=e;i.persist&&i.persist();var o=i.target,a=o.name;n=a||o.id}r.setState(function(e){return{touched:C(e.touched,n,!0)}}),r.props.validateOnBlur&&r.runValidations(r.state.values,n)};if(g(e)){var n=e;return f(r.hbCache[n])||(r.hbCache[n]=function(e){return t(e,n)}),r.hbCache[n]}t(e)},r.setFieldTouched=function(e,t,i){void 0===t&&(t=!0),void 0===i&&(i=!0),r.setState(function(r){return n.__assign({},r,{touched:C(r.touched,e,t)})},function(){r.props.validateOnBlur&&i&&r.runValidations(r.state.values,e)})},r.setFieldError=function(e,t){r.setState(function(r){return n.__assign({},r,{errors:C(r.errors,e,t)})})},r.setFieldPristine=function(e){r.setState(function(t){return n.__assign({},t,{touched:C(t.touched,e,void 0)})})},r.resetForm=function(e,t){var n=e||r.props.initialValues;r.initialValues=n,t||(t=function(){}),r.setState({isSubmitting:!1,isValidating:!1,errors:{},touched:{},error:void 0,status:r.props.initialStatus,values:n,submitCount:0},t)},r.handleReset=function(){if(r.props.onReset){var e=r.props.onReset(r.state.values,r.getFormikActions());y(e)?e.then(r.resetForm):r.resetForm()}else r.resetForm()},r.setFormikState=function(e,t){return r.setState(e,t)},r.validateForm=function(e){return r.setState({isValidating:!0}),r.runValidations(e).then(function(e){return r.didMount&&r.setState({isValidating:!1}),e})},r.getFormikActions=function(){return{resetForm:r.resetForm,submitForm:r.submitForm,validateForm:r.validateForm,validateField:r.validateField,setError:r.setError,setErrors:r.setErrors,setFieldError:r.setFieldError,setFieldTouched:r.setFieldTouched,setFieldValue:r.setFieldValue,setStatus:r.setStatus,setSubmitting:r.setSubmitting,setTouched:r.setTouched,setValues:r.setValues,setFormikState:r.setFormikState}},r.getFormikComputedProps=function(){var e=r.props.isInitialValid,t=!i(r.initialValues,r.state.values);return{dirty:t,isValid:t?r.state.errors&&0===Object.keys(r.state.errors).length:!1!==e&&f(e)?e(r.props):e,initialValues:r.initialValues}},r.getFormikBag=function(){return n.__assign({},r.state,r.getFormikActions(),r.getFormikComputedProps(),{registerField:r.registerField,unregisterField:r.unregisterField,handleBlur:r.handleBlur,handleChange:r.handleChange,handleReset:r.handleReset,handleSubmit:r.handleSubmit,validateOnChange:r.props.validateOnChange,validateOnBlur:r.props.validateOnBlur})},r.getFormikContext=function(){return n.__assign({},r.getFormikBag(),{validationSchema:r.props.validationSchema,validate:r.props.validate,initialValues:r.initialValues})},r.state={values:t.initialValues||{},errors:{},touched:{},isSubmitting:!1,isValidating:!1,submitCount:0,status:t.initialStatus},r.didMount=!1,r.fields={},r.initialValues=t.initialValues||{},u(!(t.component&&t.render),"You should not use <Formik component> and <Formik render> in the same <Formik> component; <Formik render> will be ignored"),u(!(t.component&&t.children&&!S(t.children)),"You should not use <Formik component> and <Formik children> in the same <Formik> component; <Formik children> will be ignored"),u(!(t.render&&t.children&&!S(t.children)),"You should not use <Formik render> and <Formik children> in the same <Formik> component; <Formik children> will be ignored"),r}return n.__extends(t,e),t.prototype.componentDidMount=function(){this.didMount=!0},t.prototype.componentWillUnmount=function(){this.didMount=!1,this.validator&&this.validator()},t.prototype.componentDidUpdate=function(e){this.props.enableReinitialize&&!i(e.initialValues,this.props.initialValues)&&(this.initialValues=this.props.initialValues,this.resetForm(this.props.initialValues))},t.prototype.runFieldLevelValidations=function(e,t){var n=this,r=[];t?this.fields&&this.fields[t]&&this.fields[t].props.validate&&f(this.fields[t].props.validate)&&(r=[t]):r=Object.keys(this.fields).filter(function(e){return n.fields&&n.fields[e]&&n.fields[e].props.validate&&f(n.fields[e].props.validate)});var i=r.length>0?r.map(function(t){return n.runSingleFieldLevelValidation(t,V(e,t))}):[Promise.resolve("DO_NOT_DELETE_YOU_WILL_BE_FIRED")];return Promise.all(i).then(function(e){return e.reduce(function(e,t,n){return"DO_NOT_DELETE_YOU_WILL_BE_FIRED"===t?e:(t&&(e=C(e,r[n],t)),e)},{})})},t.prototype.runValidateHandler=function(e){var t=this;return new Promise(function(n){var r=t.props.validate(e);void 0===r?n({}):y(r)?r.then(function(){n({})},function(e){n(e)}):n(r)})},t.prototype.render=function(){var e=this.props,t=e.component,n=e.render,i=e.children,o=this.getFormikBag(),a=this.getFormikContext();return r.createElement(c,{value:a},t?r.createElement(t,o):n?n(o):i?f(i)?i(o):S(i)?null:r.Children.only(i):null)},t.defaultProps={validateOnChange:!0,validateOnBlur:!0,isInitialValid:!1,enableReinitialize:!1},t}(r.Component);function E(e){var t={};if(0===e.inner.length)return C(t,e.path,e.message);for(var n=0,r=e.inner;n<r.length;n++){var i=r[n];t[i.path]||(t=C(t,i.path,i.message))}return t}function P(e,t,n,r){void 0===n&&(n=!1),void 0===r&&(r={});var i={};for(var o in e)if(e.hasOwnProperty(o)){var a=String(o);i[a]=""!==e[a]?e[a]:void 0}return t[n?"validateSync":"validate"](i,{abortEarly:!1,context:r})}function w(e,t,n){var r=e.slice();return t.forEach(function(t,i){if(void 0===r[i]){var a=!1!==n.clone&&n.isMergeableObject(t);r[i]=a?o(Array.isArray(t)?[]:{},t,n):t}else n.isMergeableObject(t)?r[i]=o(e[i],t,n):-1===e.indexOf(t)&&r.push(t)}),r}var M=m(function(e){function t(t){var n=e.call(this,t)||this,r=t.render,i=t.children,o=t.component;return u(!(o&&r),"You should not use <Field component> and <Field render> in the same <Field> component; <Field component> will be ignored"),u(!(o&&i&&f(i)),"You should not use <Field component> and <Field children> as a function in the same <Field> component; <Field component> will be ignored."),u(!(r&&i&&!S(i)),"You should not use <Field render> and <Field children> in the same <Field> component; <Field children> will be ignored"),n}return n.__extends(t,e),t.prototype.componentDidMount=function(){this.props.formik.registerField(this.props.name,this)},t.prototype.componentDidUpdate=function(e){this.props.name!==e.name&&(this.props.formik.unregisterField(e.name),this.props.formik.registerField(this.props.name,this)),this.props.validate!==e.validate&&this.props.formik.registerField(this.props.name,this)},t.prototype.componentWillUnmount=function(){this.props.formik.unregisterField(this.props.name)},t.prototype.render=function(){var e=this.props,t=e.name,i=e.render,o=e.children,a=e.component,s=void 0===a?"input":a,u=e.formik,l=n.__rest(e,["validate","name","render","children","component","formik"]),d=n.__rest(u,["validate","validationSchema"]),p={value:"radio"===l.type||"checkbox"===l.type?l.value:V(u.values,t),name:t,onChange:u.handleChange,onBlur:u.handleBlur},c={field:p,form:d};if(i)return i(c);if(f(o))return o(c);if("string"==typeof s){var h=l.innerRef,m=n.__rest(l,["innerRef"]);return r.createElement(s,n.__assign({ref:h},p,m,{children:o}))}return r.createElement(s,n.__assign({},c,l,{children:o}))},t}(r.Component)),R=m(function(e){var t=e.formik,i=t.handleReset,o=t.handleSubmit,a=n.__rest(e,["formik"]);return r.createElement("form",n.__assign({onReset:i,onSubmit:o},a))});R.displayName="Form";var j=function(e,t,n){var r=(e||[]).slice(),i=r[t];return r.splice(t,1),r.splice(n,0,i),r},A=function(e,t,n){var r=(e||[]).slice(),i=r[t];return r[t]=r[n],r[n]=i,r},B=function(e,t,n){var r=(e||[]).slice();return r.splice(t,0,n),r},U=function(e,t,n){var r=(e||[]).slice();return r[t]=n,r},D=m(function(e){function t(t){var r=e.call(this,t)||this;return r.updateArrayField=function(e,t,i){var o=r.props,a=o.name,s=o.validateOnChange,u=o.formik,l=u.validateForm;(0,u.setFormikState)(function(r){var o="function"==typeof i?i:e,s="function"==typeof t?t:e;return n.__assign({},r,{values:C(r.values,a,e(V(r.values,a))),errors:i?C(r.errors,a,o(V(r.errors,a))):r.errors,touched:t?C(r.touched,a,s(V(r.touched,a))):r.touched})},function(){s&&l()})},r.push=function(e){return r.updateArrayField(function(t){return(t||[]).concat([p(e)])},!1,!1)},r.handlePush=function(e){return function(){return r.push(e)}},r.swap=function(e,t){return r.updateArrayField(function(n){return A(n,e,t)},!0,!0)},r.handleSwap=function(e,t){return function(){return r.swap(e,t)}},r.move=function(e,t){return r.updateArrayField(function(n){return j(n,e,t)},!0,!0)},r.handleMove=function(e,t){return function(){return r.move(e,t)}},r.insert=function(e,t){return r.updateArrayField(function(n){return B(n,e,t)},function(t){return B(t,e,null)},function(t){return B(t,e,null)})},r.handleInsert=function(e,t){return function(){return r.insert(e,t)}},r.replace=function(e,t){return r.updateArrayField(function(n){return U(n,e,t)},!1,!1)},r.handleReplace=function(e,t){return function(){return r.replace(e,t)}},r.unshift=function(e){var t=-1;return r.updateArrayField(function(n){var r=n?[e].concat(n):[e];return t<0&&(t=r.length),r},function(e){var n=e?[null].concat(e):[null];return t<0&&(t=n.length),n},function(e){var n=e?[null].concat(e):[null];return t<0&&(t=n.length),n}),t},r.handleUnshift=function(e){return function(){return r.unshift(e)}},r.handleRemove=function(e){return function(){return r.remove(e)}},r.handlePop=function(){return function(){return r.pop()}},r.remove=r.remove.bind(r),r.pop=r.pop.bind(r),r}return n.__extends(t,e),t.prototype.remove=function(e){var t;return this.updateArrayField(function(n){var r=n?n.slice():[];return t||(t=r[e]),f(r.splice)&&r.splice(e,1),r},!0,!0),t},t.prototype.pop=function(){var e;return this.updateArrayField(function(t){var n=t;return e||(e=n&&n.pop&&n.pop()),n},!0,!0),e},t.prototype.render=function(){var e={push:this.push,pop:this.pop,swap:this.swap,move:this.move,insert:this.insert,replace:this.replace,unshift:this.unshift,remove:this.remove,handlePush:this.handlePush,handlePop:this.handlePop,handleSwap:this.handleSwap,handleMove:this.handleMove,handleInsert:this.handleInsert,handleReplace:this.handleReplace,handleUnshift:this.handleUnshift,handleRemove:this.handleRemove},t=this.props,i=t.component,o=t.render,a=t.children,s=t.name,u=t.formik,l=n.__rest(u,["validate","validationSchema"]),d=n.__assign({},e,{form:l,name:s});return i?r.createElement(i,d):o?o(d):a?"function"==typeof a?a(d):S(a)?null:r.Children.only(a):null},t.defaultProps={validateOnChange:!0},t}(r.Component)),T=m(function(e){function t(t){var n=e.call(this,t)||this,r=t.render,i=t.children,o=t.component;return u(!(o&&r),"You should not use <FastField component> and <FastField render> in the same <FastField> component; <FastField component> will be ignored"),u(!(o&&i&&f(i)),"You should not use <FastField component> and <FastField children> as a function in the same <FastField> component; <FastField component> will be ignored."),u(!(r&&i&&!S(i)),"You should not use <FastField render> and <FastField children> in the same <FastField> component; <FastField children> will be ignored"),n}return n.__extends(t,e),t.prototype.shouldComponentUpdate=function(e){return this.props.shouldUpdate?this.props.shouldUpdate(e,this.props):V(this.props.formik.values,this.props.name)!==V(e.formik.values,this.props.name)||V(this.props.formik.errors,this.props.name)!==V(e.formik.errors,this.props.name)||V(this.props.formik.touched,this.props.name)!==V(e.formik.touched,this.props.name)||Object.keys(this.props).length!==Object.keys(e).length||this.props.formik.isSubmitting!==e.formik.isSubmitting},t.prototype.componentDidMount=function(){this.props.formik.registerField(this.props.name,this)},t.prototype.componentDidUpdate=function(e){this.props.name!==e.name&&(this.props.formik.unregisterField(e.name),this.props.formik.registerField(this.props.name,this)),this.props.validate!==e.validate&&this.props.formik.registerField(this.props.name,this)},t.prototype.componentWillUnmount=function(){this.props.formik.unregisterField(this.props.name)},t.prototype.render=function(){var e=this.props,t=e.name,i=e.render,o=e.children,a=e.component,s=void 0===a?"input":a,u=e.formik,l=n.__rest(e,["validate","name","render","children","component","formik","shouldUpdate"]),d=n.__rest(u,["validate","validationSchema"]),p={value:"radio"===l.type||"checkbox"===l.type?l.value:V(u.values,t),name:t,onChange:u.handleChange,onBlur:u.handleBlur},c={field:p,form:d};if(i)return i(c);if(f(o))return o(c);if("string"==typeof s){var h=l.innerRef,m=n.__rest(l,["innerRef"]);return r.createElement(s,n.__assign({ref:h},p,m,{children:o}))}return r.createElement(s,n.__assign({},c,l,{children:o}))},t}(r.Component)),I=m(function(e){function t(){return null!==e&&e.apply(this,arguments)||this}return n.__extends(t,e),t.prototype.shouldComponentUpdate=function(e){return V(this.props.formik.errors,this.props.name)!==V(e.formik.errors,this.props.name)||V(this.props.formik.touched,this.props.name)!==V(e.formik.touched,this.props.name)||Object.keys(this.props).length!==Object.keys(e).length},t.prototype.render=function(){var e=this.props,t=e.component,i=e.formik,o=e.render,a=e.children,s=e.name,u=n.__rest(e,["component","formik","render","children","name"]),l=V(i.touched,s),d=V(i.errors,s);return l&&d?o?f(o)?o(d):null:a?f(a)?a(d):null:t?r.createElement(t,u,d):d:null},t}(r.Component));exports.ErrorMessage=I,exports.FastField=T,exports.Field=M,exports.FieldArray=D,exports.Form=R,exports.Formik=O,exports.FormikConsumer=h,exports.FormikProvider=c,exports.connect=m,exports.getActiveElement=function(e){if(void 0===(e=e||("undefined"!=typeof document?document:void 0)))return null;try{return e.activeElement||e.body}catch(t){return e.body}},exports.getIn=V,exports.insert=B,exports.isEmptyChildren=S,exports.isFunction=f,exports.isInputEvent=_,exports.isInteger=F,exports.isNaN=b,exports.isObject=v,exports.isPromise=y,exports.isString=g,exports.makeCancelable=k,exports.move=j,exports.replace=U,exports.setIn=C,exports.setNestedObjectValues=x,exports.swap=A,exports.validateYupSchema=P,exports.withFormik=function(e){var t=e.mapPropsToValues,i=void 0===t?function(e){var t={};for(var n in e)e.hasOwnProperty(n)&&"function"!=typeof e[n]&&(t[n]=e[n]);return t}:t,o=n.__rest(e,["mapPropsToValues"]);return function(e){var t=e.displayName||e.name||e.constructor&&e.constructor.name||"Component",s=function(a){function s(){var t=null!==a&&a.apply(this,arguments)||this;return t.validate=function(e){return o.validate(e,t.props)},t.validationSchema=function(){return f(o.validationSchema)?o.validationSchema(t.props):o.validationSchema},t.handleSubmit=function(e,r){return o.handleSubmit(e,n.__assign({},r,{props:t.props}))},t.renderFormComponent=function(i){return r.createElement(e,n.__assign({},t.props,i))},t}return n.__extends(s,a),s.prototype.render=function(){var e=this.props,t=n.__rest(e,["children"]);return r.createElement(O,n.__assign({},t,o,{validate:o.validate&&this.validate,validationSchema:o.validationSchema&&this.validationSchema,initialValues:i(this.props),initialStatus:o.mapPropsToStatus&&o.mapPropsToStatus(this.props),onSubmit:this.handleSubmit,render:this.renderFormComponent}))},s.displayName="WithFormik("+t+")",s}(r.Component);return a(s,e)}},exports.yupToFormErrors=E;
//# sourceMappingURL=formik-custom.cjs.production.js.map
