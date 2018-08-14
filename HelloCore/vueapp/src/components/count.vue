<template>
  <div id="Count">
    <div class="Count_Top">
      <label>当前历史记账金额</label><br>
      <label class="Money">{{totalCount}}</label>
    </div>
    <div>
      <mt-navbar v-model="selected">
        <router-link to="/Count" v-bind:class="classmenumonth" v-on:click.native="select(0)">当月</router-link>
        <router-link to="/Count/Year" v-bind:class="classmenuyear" v-on:click.native="select(1)">当年</router-link>
      </mt-navbar>
      <router-view></router-view>
    </div>
  </div>
</template>
<script>
import {Toast} from 'mint-ui';
export default {
  data(){
    return{
      selected:-1,
      totalCount:0,
    }
  },
  methods: {
    select(m){
      this.selected = m;
    },
    GetTotalCount(){
      this.$ajax.get('http://localhost:51846/api/bill/GetTotalCount')
      .then(response=>{
        this.totalCount = response.data
      })
      .catch(ex=>{
        Toast(ex);
      })
    }
  },
  created(){
    this.GetTotalCount();
  },
  computed:{
    classmenumonth(){
      return{
        'mint-tab-item':true,
        'is-selected':this.selected==0,
      }
    },
    classmenuyear(){
      return{
        'mint-tab-item':true,
        'is-selected':this.selected==1,
      }
    }
  }
}
</script>
<style scoped>
  #Count .Count_Top{
    padding: 16px;
    color: #26a2ff;
  }
  #Count .Count_Top label{
    font-size: 12px;
  }
  #Count .Count_Top .Money{
    font-size: 30px;
  }
  #Count{
    margin-top: 0;
    position: relative;
    height: auto;
    background-color: #eee;}
  .mint-tab-item{
    padding: 12px;
  }
</style>