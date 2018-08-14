<template>
    <div>
        <div id="TopTitle">
            <mt-header v-bind:title="msg">
            </mt-header>
            <mt-field label="￥" aria-placeholder="输入金额" type="number" v-model="money"></mt-field>
        </div>
        <div id="BillTypes">
            <div v-for="item in billType" :key="item.id" class="item" v-on:click="Add(item.id)">
                <div class="item_img">
                    <i class="fa fa-3x" v-bind:class="item.fontStyle"></i>
                </div>
                <span>{{item.name}}</span>
            </div>
            <div style="clear: both"></div>
        </div>
    </div>
</template>

<script>
import { Toast } from 'mint-ui';
export default {
    name: 'record',
    data() {
        return {
            msg:'从记账开始',
            billType: [],
            money: '',
        }
    },
    created(){
        this.$ajax.get('http://localhost:51846/api/bill/GetBillType')
        .then(r => {
            this.billType = r.data;
        })
        .catch(ex=>{
            Toast(""+ex)
        })
    },
    methods: {
        Add(m){
            if(this.money == ''){
                Toast('先金额，后去向');
                return;
            }
            this.$ajax.post('http://localhost:51846/api/bill/AddBills',{
                Money: this.money,
                BillTypeId: m,
            })
            .then(response=>{
                    Toast({
                        message:'记账成功',
                        iconClass:'icon icon-success'
                    });
                    this.money = '';
            })
            .catch(ex=>{
                Toast(ex);
            });
        },
    },
}
</script>
<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  #TopTitle{
    position: fixed;
    top: 0;
    width: 100%;
    background-color: #eee;
    z-index: 1;
  }
  #TopTitle div *{
    border-style: none;
  }
  #BillTypes{
    margin-top: 88px;
    position: relative;
    height: auto;
    background-color: #eee;
  }
  #BillTypes .item{
    height: 100px;
    padding: 11px 15px;
    vertical-align: top;
    border-right: 1px solid #fff;
    border-bottom: 1px solid #fff;
    position: relative;
    float: left;
    width: 33.33333%;
    box-sizing: border-box;
  }
  #BillTypes .item .item_img{
    clear: both;
    padding-bottom: 8px;
  }
</style>
