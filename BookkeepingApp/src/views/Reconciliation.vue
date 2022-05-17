<template>
  <div class="row">
    <div class="content">
      <div class="col-md-3" style="font-family: cursive">
        <label>Choose Year</label>
        <Select
          :options="years"
          v-model="selectedYear"
          :searchable="true"
          type="text"
          autocomplete="off"
          class="form-control"
          placeholder="Select"
          @click="selectYear()"
        />
      </div>
      <form>
        <div class="col-md-12 form-group">
          <table class="table table-bordered table-responsive">
            <thead>
              <tr>
                <th colspan="14" style="text-align: center">
                  Year {{ this.selectedYear }}
                </th>
              </tr>
            </thead>

            <tbody>
              <tr>
                <th></th>
                <th></th>
                <th>Jan</th>
                <th>Feb</th>
                <th>Mar</th>
                <th>Apr</th>
                <th>May</th>
                <th>Jun</th>
                <th>Jul</th>
                <th>Aug</th>
                <th>Sep</th>
                <th>Oct</th>
                <th>Nov</th>
                <th>Dec</th>
              </tr>
              <tr v-for="(item, idx) in predefinedIncomeCostList" :key="idx">
                <td></td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ idx == "income" ? "Income" : "" }}
                  {{ idx == "cumulativeIncome" ? "Cumulative Income" : "" }}
                  {{ idx == "cost" ? "Cost" : "" }}
                  {{ idx == "cumulativeCost" ? "Cumulative Cost" : "" }}
                  {{ idx == "result" ? "Result" : "" }}
                </td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ item.jan != null ? item.jan : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ item.feb != null ? item.feb : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ item.mar != null ? item.mar : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ item.apr != null ? item.apr : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ item.may != null ? item.may : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ item.jun != null ? item.jun : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ item.jul != null ? item.jul : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ item.aug != null ? item.aug : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ item.sep != null ? item.sep : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ item.oct != null ? item.oct : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ item.nov != null ? item.nov : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'dark-gray' : '']">
                  {{ item.dec != null ? item.dec : 0 }}
                </td>
              </tr>
              <tr>
                <th></th>
                <th colspan="13" style="text-align: center">Reconciliation</th>
              </tr>
              <template v-if="reconciliationList.length > 0"> 
              <tr v-for="(item2, idx2) in reconciliationList" :key="idx2">
                <td
                  v-if="
                    item2.details == actionWiseTypes[0].typeName.toUpperCase() ||
                    item2.details == actionWiseTypes[1].typeName.toUpperCase()
                  "
                  :rowspan="item2.numOfTypes"
                >
                  {{ item2.action || item2.actionName.toUpperCase() }}
                </td>
                <td :class="[idx2 != 'result' ? 'dark-gray' : '']">
                  {{ item2.details || item2.typeName.toUpperCase() }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'jan')"
                  @keyup.delete="onRemove(idx2)"
                  :class="[idx2 != 'result' ? 'dark-gray' : '']"
                >
                  {{ item2.jan }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'feb')"
                  @keyup.delete="onRemove(idx2)"
                  :class="[idx2 != 'result' ? 'dark-gray' : '']"
                >
                  {{ item2.feb }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'mar')"
                  @keyup.delete="onRemove(idx2)"
                  :class="[idx2 != 'result' ? 'dark-gray' : '']"
                >
                  {{ item2.mar }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'apr')"
                  @keyup.delete="onRemove(idx2)"
                  :class="[idx2 != 'result' ? 'dark-gray' : '']"
                >
                  {{ item2.apr }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'may')"
                  @keyup.delete="onRemove(idx2)"
                  :class="[idx2 != 'result' ? 'dark-gray' : '']"
                >
                  {{ item2.may }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'jun')"
                  @keyup.delete="onRemove(idx2)"
                  :class="[idx2 != 'result' ? 'dark-gray' : '']"
                >
                  {{ item2.jun }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'jul')"
                  @keyup.delete="onRemove(idx2)"
                  :class="[idx2 != 'result' ? 'dark-gray' : '']"
                >
                  {{ item2.jul }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'aug')"
                  @keyup.delete="onRemove(idx2)"
                  :class="[idx2 != 'result' ? 'dark-gray' : '']"
                >
                  {{ item2.aug }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'sep')"
                  @keyup.delete="onRemove(idx2)"
                  :class="[idx2 != 'result' ? 'dark-gray' : '']"
                >
                  {{ item2.sep }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'oct')"
                  @keyup.delete="onRemove(idx2)"
                  :class="[idx2 != 'result' ? 'dark-gray' : '']"
                >
                  {{ item2.oct }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'nov')"
                  @keyup.delete="onRemove(idx2)"
                  :class="[idx2 != 'result' ? 'dark-gray' : '']"
                >
                  {{ item2.nov }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'dec')"
                  @keyup.delete="onRemove(idx2)"
                  :class="[idx2 != 'result' ? 'dark-gray' : '']"
                >
                  {{ item2.dec }}
                </td>
              </tr>
              </template>
            </tbody>
          </table>
        </div>
        <div class="col-md-3">
          
            <button @click="updateReconciliations" type="button" class="button">
              Save
            </button>
         
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import Select from "@vueform/multiselect";
import Datepicker from "vue3-datepicker";
import moment from "moment";
import BookkeepingService from "../services/BookkeepingService";

export default {
  name: "Reconciliation",
  components: {
    Select,
    Datepicker,
  },
  async mounted() {
    await this.getRecordTypesList();  
    await this.getActionWiseOneTypeList();
    this.recordTypesList.forEach( itm =>
    {
      var obj = {};
      obj.action = itm.actionName.toUpperCase();
      obj.details = itm.typeName.toUpperCase();
      obj.year =  Number(this.selectedYear); 
      obj.typeId = Number(itm.id);
      obj.numOfTypes = Number(this.recordTypesList.filter(function(e){ return e.actionName == itm.actionName;}).length);
      this.reconciliationList.push(obj);
    });
  },
  data() {
    return {
      selectedYear: null,
      rowSpan: {},
      years: ["2018", "2019", "2020", "2021", "2022"],
      predefinedIncomeCostList: {
        income: {},
        cumulativeIncome: {},
        cost:{},
        cumulativeCost:{},
        result:{},
      },
      reconciliationList: [],
      actionNames: ["income", "expense"],
      recordTypesList:[],
      actionWiseTypes: []
    };
  },
  methods: {
    onInput(event, index, id) {
      const value = event.target.innerText;
      this.reconciliationList[index][id] = Number(value);
    },
    onRemove(index) {
      if (this.reconciliationList[index] >= 1) {
        this.reconciliationList[index] = 0;
      }
    },
    async selectYear() {
      if (this.selectedYear != null) {
        await this.getIncomCostList();
        await this.getReconciliationList();
      }
      
    },
    async getIncomCostList() {
      let response = await BookkeepingService.getPredefinedRecordsByYear(
        this.selectedYear
      );
      if (response.list.cost != null) {
        this.predefinedIncomeCostList = response.list;
      }
      else
      {
        this.predefinedIncomeCostList = {};
        this.predefinedIncomeCostList = {
        income: {},
        cumulativeIncome: {},
        cost:{},
        cumulativeCost:{},
        result:{},
        }
      }
    },
    async getReconciliationList() {
      let response = await BookkeepingService.getAllReconciliationsByYear(
        this.selectedYear
      );
      if (response.list.length > 0){
        this.reconciliationList = response.list;
        for (let i = 0; i < this.reconciliationList.length; i++) 
        {
          var obj={};
          obj.action = this.reconciliationList[i].action;
          this.reconciliationList[i].numOfTypes = Number(this.recordTypesList.filter(function(e){ return e.actionName == obj.action.toLowerCase();}).length);
        }
      }
      else
      {
        this.reconciliationList= [];
        this.recordTypesList.forEach( itm => 
        {
          var obj = {};
          obj.action = itm.actionName.toUpperCase();
          obj.details = itm.typeName.toUpperCase();
          obj.year =  Number(this.selectedYear); 
          obj.typeId = Number(itm.id);
          obj.numOfTypes = Number(this.recordTypesList.filter(function(e){ return e.actionName == itm.actionName;}).length);
          this.reconciliationList.push(obj);
        });
      }
    },
    async getRecordTypesList() {
      let response = await BookkeepingService.getAllRecordTypes();
      if (response) {
        this.recordTypesList = response.list;
      }
    },
    async getActionWiseOneTypeList() {
      let response = await BookkeepingService.getActionWiseTypes();
      if (response) {
        this.actionWiseTypes = response.list;
      }
    },
    async updateReconciliations() {
     
      let jsonData = {};
      jsonData.ReconciliationRecordList = this.reconciliationList;
      let response = await BookkeepingService.Reconcile(jsonData);
      if (response) {
        await this.getReconciliationList();
        this.$toast.success(response.message);
        
      }
    },
  },
};
</script>

<style scoped>
table {
  width: 100%;
}

a {
  cursor: pointer;
}

.year-dropdown {
  margin-top: 100px;
}
.year-dropdown .dropdown-menu {
  min-width: 8rem !important;
  padding: 0 !important;
}
.year-dropdown .dropdown-divider {
  margin: 0 !important;
}
.table-design table thead tr th,
tbody tr th {
  text-align: center;
}
.slim-gray {
  background-color: #f2f2f2;
}
.dark-gray {
  background-color: #d9d9d9;
  text-align: right;
}
.table thead th,
.table-bordered td,
.table-bordered th {
  border-color: #000 !important;
}
.thin-red {
  background-color: #fff2cc;
}

@media (max-width: 575.98px) {
  .year-dropdown {
    margin-top: 50px;
  }
}
</style>