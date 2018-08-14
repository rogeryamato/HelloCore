<template>
    <div id="MonthCount">
        <div id="chartbox" class="chart">
            <div id="chart"></div>
        </div>
        <ul class="list">
            <li v-for="item in listData" :key="item.id">
                <mt-cell-swipe v-bind:title="item.name" v-bind:label="item.creationTime" :right="
                [{Content:'删除',style:{background:'red',color:'#fff'},
                handler: ()=> DeleteBill(item.id)}]">
                    <span>￥{{item.money}}</span>
                    <i slot="icon" class="fa" v-bind:class="item.fontStyle" width="24" height="24"></i>
                </mt-cell-swipe>
            </li>
            <li v-if="loading" style="text-align: center">
                <mt-spinner :type="3" color="#26a2ff"></mt-spinner>
            </li>
            <li v-else-if="loadMore" v-on:click="GetListData" style="text-align: center">
                加载更多
            </li>
        </ul>
    </div>
</template>
<script>
import {Toast} from 'mint-ui';
// 引入基本模板
let echarts = require('echarts/lib/echarts')
// 引入饼图组件
require('echarts/lib/chart/pie')
// 引入提示框和图例组件
require('echarts/lib/component/tooltip')
require('echarts/lib/component/legend')
export default {
    data(){
        return{
            listData:[],
            chartData:[],
            date:new Date(),
            loadMore:false,
            loading:false
        }
    },
    methods:{
        DeleteBill(id){
            this.$ajax.post('http://localhost:51846/api/bill/DeleteBill',{
                id: id
            })
            .then(response=>{
                Toast("删除成功");
                this.GetChartData();
                this.GetListData();
            })
            .catch(ex=>{
                Toast(ex);
            })
        },
        GetChartData(){
            this.$ajax.post('http://localhost:51846/api/bill/GetCount',{
                date: this.date,
            })
            .then(response=>{
                this.chartData = response.data;
            })
            .catch(ex=>{
                Toast(ex);
            })
        },
        GetListData(){
            this.loading=true;
            this.$ajax.post('http://localhost:51846/api/bill/GetBills',{
                date:this.date,
                skip:this.listData.length
            })
            .then(response=>{
                if(response.status == "200")
                {
                    let newList = [...this.listData,...response.data.items];
                    this.listData = newList;
                    this.loadMore = response.data.totalCount > this.listData.length;
                }
                this.loading = false;
            })
            .catch(ex=>{
                this.loading=false;
                Toast(ex);
            })
        },
        DrawChart(){
            let chartbox = document.getElementById('chartbox');
            let chart = document.getElementById('chart');
            
            let myChart = echarts.init(chart);
            let data = this.chartData;
            let count =0;
            for (let i of this.chartData){
                count +=i.value;
            }

            myChart.setOption({
                title:{
                    text:count,
                    subtext:'单位(元)',
                    x:'right'
                },
                tooltip:{
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c} ({d}%)"
                },
                series: [
                    {
                        name:'消费',
                        type:'pie',
                        radius:'55%',
                        center:['50%','50%'],
                        data:data
                    }
                ]
            });
        },
    },
    watch:{
        chartData: function(){
            this.DrawChart();
        }
    },
    created(){
        this.GetChartData();
        this.GetListData();
    }
}
</script>
<style scoped>
  #chart{
    height: 300px;
    width: 100%;
  }
  ul{
    list-style: none;
    padding-left: 0;
  }
  ul li{
    text-align: left;
    margin-bottom: 5px;
  }
  .bottom{
    position: fixed;
    height: 1px;
    width: 100%;
    bottom: 65px;
  }
</style>