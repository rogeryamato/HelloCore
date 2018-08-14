<template>
  <div id="app">
    <router-view class="Content"/>
    <mt-tabbar :fixed="true">
      <router-link to="/" v-bind:class="classmenuhello" v-on:click.native="select(0)" > <i class="fa fa-edit"></i> 记账 </router-link>
      <router-link to="/count"  v-bind:class="classmenucount" v-on:click.native="select(1)"> <i class="fa fa-bar-chart"></i> 统计 </router-link>
    </mt-tabbar>

  </div>
</template>

<script>
export default {
  name: 'App',
  data(){
    return {
      selected:-1,
    }
  },
  computed:{
    classmenuhello : function () {
      return {
        'mint-tab-item':true,
        'is-selected':this.selected==0,
      }
    },
    classmenucount : function () {
      return {
        'mint-tab-item':true,
        'is-selected':this.selected==1,
      }
    }
  },
  methods:{
    select(m){
      this.selected=m;
    }
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
#app .mint-tab-item{
  font-size: 20px;
  padding: 15px;
}
#app .Content{
  padding-bottom: 56px;
}
</style>
