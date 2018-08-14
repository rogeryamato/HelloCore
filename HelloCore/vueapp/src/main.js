// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import Vuex from 'vuex'
import axios from 'axios'
import VueResouse from 'vue-resource' // 引入vue-resource
import MintUI from 'mint-ui'     // 引入mint-ui
import 'mint-ui/lib/style.css'  // 引入mint-ui的样式
// import echarts from 'echarts'

Vue.prototype.$ajax = axios
// Vue.prototype.$echarts = echarts
Vue.use(Vuex)
Vue.use(MintUI);
Vue.use(VueResouse);
Vue.config.productionTip = false

Vue.prototype.$goRoute = function(index){
  this.$router.push(index)
}


/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
