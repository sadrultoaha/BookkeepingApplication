<template>
  <div class="row">
    <div class="content">
      <div class="col-md-3" style="font-family: cursive">
        <label style="font-size: 18px">Choose Year</label>
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
      <br />
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
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ idx == "income" ? "Income" : "" }}
                  {{ idx == "cumulativeIncome" ? "Cumulative Income" : "" }}
                  {{ idx == "cost" ? "Cost" : "" }}
                  {{ idx == "cumulativeCost" ? "Cumulative Cost" : "" }}
                  {{ idx == "result" ? "Result" : "" }}
                </td>
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ item.jan != null ? item.jan : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ item.feb != null ? item.feb : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ item.mar != null ? item.mar : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ item.apr != null ? item.apr : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ item.may != null ? item.may : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ item.jun != null ? item.jun : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ item.jul != null ? item.jul : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ item.aug != null ? item.aug : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ item.sep != null ? item.sep : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ item.oct != null ? item.oct : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ item.nov != null ? item.nov : 0 }}
                </td>
                <td :class="[idx != 'result' ? 'fixed-color' : '']">
                  {{ item.dec != null ? item.dec : 0 }}
                </td>
              </tr>
              <tr>
                <th></th>
                <th colspan="13" style="text-align: center">Reconciliation</th>
              </tr>
              <tr v-for="(item2, idx2) in reconciliationList" :key="idx2">
                <td
                  v-if="
                    item2.typeName.toUpperCase() ==
                      actionWiseTypes[0].typeName.toUpperCase() ||
                    item2.typeName.toUpperCase() ==
                      actionWiseTypes[1].typeName.toUpperCase()
                  "
                  :rowspan="item2.numOfTypes"
                >
                  {{ item2.actionName.toUpperCase() }}
                </td>
                <td>
                  {{ item2.typeName.toUpperCase() }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'jan')"
                  @keyup.delete="onRemove(idx2)"
                  class="edit-color"
                >
                  {{ item2.jan }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'feb')"
                  @keyup.delete="onRemove(idx2)"
                  class="edit-color"
                >
                  {{ item2.feb }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'mar')"
                  @keyup.delete="onRemove(idx2)"
                  class="edit-color"
                >
                  {{ item2.mar }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'apr')"
                  @keyup.delete="onRemove(idx2)"
                  class="edit-color"
                >
                  {{ item2.apr }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'may')"
                  @keyup.delete="onRemove(idx2)"
                  class="edit-color"
                >
                  {{ item2.may }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'jun')"
                  @keyup.delete="onRemove(idx2)"
                  class="edit-color"
                >
                  {{ item2.jun }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'jul')"
                  @keyup.delete="onRemove(idx2)"
                  class="edit-color"
                >
                  {{ item2.jul }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'aug')"
                  @keyup.delete="onRemove(idx2)"
                  class="edit-color"
                >
                  {{ item2.aug }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'sep')"
                  @keyup.delete="onRemove(idx2)"
                  class="edit-color"
                >
                  {{ item2.sep }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'oct')"
                  @keyup.delete="onRemove(idx2)"
                  class="edit-color"
                >
                  {{ item2.oct }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'nov')"
                  @keyup.delete="onRemove(idx2)"
                  class="edit-color"
                >
                  {{ item2.nov }}
                </td>
                <td
                  contenteditable
                  @input="(event) => onInput(event, idx2, 'dec')"
                  @keyup.delete="onRemove(idx2)"
                  class="edit-color"
                >
                  {{ item2.dec }}
                </td>
              </tr>
              <tr v-for="(item3, idx3) in reconciliationResults" :key="idx3">
                <td></td>
                <td>
                  {{ item3.typeName }}
                </td>
                <td>
                  {{ item3.jan != null ? item3.jan : 0 }}
                </td>
                <td>
                  {{ item3.feb != null ? item3.feb : 0 }}
                </td>
                <td>
                  {{ item3.mar != null ? item3.mar : 0 }}
                </td>
                <td>
                  {{ item3.apr != null ? item3.apr : 0 }}
                </td>
                <td>
                  {{ item3.may != null ? item3.may : 0 }}
                </td>
                <td>
                  {{ item3.jun != null ? item3.jun : 0 }}
                </td>
                <td>
                  {{ item3.jul != null ? item3.jul : 0 }}
                </td>
                <td>
                  {{ item3.aug != null ? item3.aug : 0 }}
                </td>
                <td>
                  {{ item3.sep != null ? item3.sep : 0 }}
                </td>
                <td>
                  {{ item3.oct != null ? item3.oct : 0 }}
                </td>
                <td>
                  {{ item3.nov != null ? item3.nov : 0 }}
                </td>
                <td>
                  {{ item3.dec != null ? item3.dec : 0 }}
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </form>
      <div class="row justify-content-center">
        <button
          @click="updateReconciliations"
          type="button"
          class="btn btn-success"
        >
          Save
        </button>
      </div>
      <br />
    </div>
  </div>
</template>

<script>
import Select from "@vueform/multiselect";
import BookkeepingService from "../services/BookkeepingService";

export default {
  name: "Reconciliation",
  components: {
    Select,
  },
  async mounted() {
    await this.getRecordTypesList();
    await this.getActionWiseOneTypeList();

    this.recordTypesList.forEach((itm) => {
      var obj = {};
      obj.actionName = itm.actionName.toUpperCase();
      obj.typeName = itm.typeName.toUpperCase();
      obj.year = Number(this.selectedYear);
      obj.typeId = Number(itm.id);
      obj.numOfTypes = Number(
        this.recordTypesList.filter(function (e) {
          return e.actionName.toUpperCase() == itm.actionName.toUpperCase();
        }).length
      );
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
        cost: {},
        cumulativeCost: {},
        result: {},
      },
      reconciliationList: [],
      reconciliationResults: [],
      actionNames: ["INCOME", "EXPENSE"],
      recordTypesList: [],
      actionWiseTypes: [],
    };
  },
  methods: {
    async onInput(event, index, id) {
      const value = event.target.innerText;
      this.reconciliationList[index][id] = Number(value);
      if (this.reconciliationList[index][id] > 0) {
        this.reconciliationResults = [];
        await this.calcReconciliationResults();
      }
    },
    async onRemove(index) {
      if (this.reconciliationList[index] >= 1) {
        this.reconciliationList[index] = 0;
        this.reconciliationResults = [];
        await this.calcReconciliationResults();
      }
    },
    async selectYear() {
      if (this.selectedYear != null) {
        await this.getIncomCostList();
        await this.getReconciliationList();
      }
      if (
        this.predefinedIncomeCostList.cost != null &&
        this.reconciliationList != null
      ) {
        this.reconciliationResults = [];
        await this.calcReconciliationResults();
      }
    },
    async getIncomCostList() {
      let response = await BookkeepingService.getPredefinedRecordsByYear(
        this.selectedYear
      );
      if (response.list.cost != null) {
        this.predefinedIncomeCostList = response.list;
      } else {
        this.predefinedIncomeCostList = {};
        this.predefinedIncomeCostList = {
          income: {},
          cumulativeIncome: {},
          cost: {},
          cumulativeCost: {},
          result: {},
        };
      }
    },
    async getReconciliationList() {
      let response = await BookkeepingService.getAllReconciliationsByYear(
        this.selectedYear
      );
      if (response.list.length > 0) {
        this.reconciliationList = response.list;
        for (let i = 0; i < this.reconciliationList.length; i++) {
          var obj = {};
          obj.actionName = this.reconciliationList[i].actionName;
          this.reconciliationList[i].numOfTypes = Number(
            this.recordTypesList.filter(function (e) {
              return e.actionName.toUpperCase() == obj.actionName.toUpperCase();
            }).length
          );
        }
      } else {
        this.reconciliationList = [];
        this.recordTypesList.forEach((itm) => {
          var obj = {};
          obj.actionName = itm.actionName.toUpperCase();
          obj.typeName = itm.typeName.toUpperCase();
          obj.year = Number(this.selectedYear);
          obj.typeId = Number(itm.id);
          obj.numOfTypes = Number(
            this.recordTypesList.filter(function (e) {
              return e.actionName == itm.actionName;
            }).length
          );
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
    calcReconciliationResults() {
      let sumsArray = {};
      this.reconciliationList.forEach((item) => {
        let sums = sumsArray[item.actionName];
        if (sums) {
          sums.jan += item.jan;
          sums.feb += item.feb;
          sums.mar += item.mar;
          sums.apr += item.apr;
          sums.may += item.may;
          sums.jun += item.jun;
          sums.jul += item.jul;
          sums.aug += item.aug;
          sums.sep += item.sep;
          sums.oct += item.oct;
          sums.nov += item.nov;
          sums.dec += item.dec;
        } else {
          sumsArray[item.actionName] = {
            typeName: item.typeName,
            actionName: item.actionName,
            jan: item.jan,
            feb: item.feb,
            mar: item.mar,
            apr: item.apr,
            may: item.may,
            jun: item.jun,
            jul: item.jul,
            aug: item.aug,
            sep: item.sep,
            oct: item.oct,
            nov: item.nov,
            dec: item.dec,
          };
        }
      });

      this.reconciliationResults.push(
        this.calcArrays(
          sumsArray.INCOME,
          sumsArray.EXPENSE,
          "Reconciliation Result",
          "opSub"
        )
      );

      this.reconciliationResults.push(
        this.calcArrays(
          this.predefinedIncomeCostList.result,
          this.reconciliationResults[0],
          "Final Result",
          "opAdd"
        )
      );

      this.reconciliationResults.push(
        this.calcCumulativeSum(
          this.reconciliationResults[1],
          "Cumulative Final Result"
        )
      );
    },
    calcArrays(a, b, typeName, op) {
      function calcObjectsByKey(...objs) {
        return objs.reduce((a, b) => {
          for (let k in b) {
            if (k == "typeName") {
              a[k] = typeName;
            } else if (b.hasOwnProperty(k)) {
              if (op == "opSub") a[k] = -(a[k] || 0) - (b[k] || 0);
              else a[k] = (a[k] || 0) + (b[k] || 0);
            }
          }
          return a;
        }, {});
      }
      return calcObjectsByKey(a, b);
    },
    calcCumulativeSum(a, typeName) {
      let res = {};
      res.typeName = typeName;
      res.jan = a.jan || 0;
      res.feb = a.jan + a.feb || 0;
      res.mar = a.jan + a.feb + a.mar || 0;
      res.apr = a.jan + a.feb + a.mar + a.apr || 0;
      res.may = a.jan + a.feb + a.mar + a.apr + a.may || 0;
      res.jun = a.jan + a.feb + a.mar + a.apr + a.may + a.jun || 0;
      res.jul = a.jan + a.feb + a.mar + a.apr + a.may + a.jun + a.jul || 0;
      res.aug =
        a.jan + a.feb + a.mar + a.apr + a.may + a.jun + a.jul + a.aug || 0;
      res.sep =
        a.jan + a.feb + a.mar + a.apr + a.may + a.jun + a.jul + a.aug + a.sep ||
        0;
      res.oct =
        a.jan +
          a.feb +
          a.mar +
          a.apr +
          a.may +
          a.jun +
          a.jul +
          a.aug +
          a.sep +
          a.oct || 0;
      res.nov =
        a.jan +
          a.feb +
          a.mar +
          a.apr +
          a.may +
          a.jun +
          a.jul +
          a.aug +
          a.sep +
          a.oct +
          a.nov || 0;
      res.dec =
        a.jan +
          a.feb +
          a.mar +
          a.apr +
          a.may +
          a.jun +
          a.jul +
          a.aug +
          a.sep +
          a.oct +
          a.nov +
          a.dec || 0;

      return res;
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
.fixed-color {
  background-color: #d9d9d9;
  text-align: right;
}
.table thead th,
.table-bordered td,
.table-bordered th {
  border-color: black;
}
.edit-color {
  background-color: #fff2cc;
}
</style>