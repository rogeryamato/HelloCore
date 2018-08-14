import Vue from 'vue'
import Router from 'vue-router'
import record from '@/components/record'
import count from '@/components/count'
import monthCount from '@/components/MonthCount'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'record',
      component: record
    },
    {
      path: '/count',
      name: 'count',
      component:count,
      children:[
        {path:'',component:monthCount}
      ]
    }
  ]
})
