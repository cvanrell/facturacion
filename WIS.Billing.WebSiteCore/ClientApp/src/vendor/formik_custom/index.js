'use strict'

if (process.env.NODE_ENV === 'production') {
  module.exports = require('./formik-custom.cjs.production.js');
} else {
  module.exports = require('./formik-custom.cjs.development.js');
}