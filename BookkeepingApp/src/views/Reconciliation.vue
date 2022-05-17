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
                <th colspan="14" style="text-align:center">Year {{ this.selectedYear }}</th>
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
              <tr v-for="(item, k) in PredefinedIncomeCostList" :key="k">
                <td></td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.details }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.jan }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.feb }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.mar }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.apr }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.may }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.jun }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.jul }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.aug }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.sep }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.oct }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.nov }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item.dec }}</td>
              </tr>
              <tr>
                <th></th>
                <th colspan="14" style="text-align:center">Reconciliation</th>
              </tr>
              <tr v-for="(item2, k2) in ReconciliationList" :key="k2">
                <td rowspan="item2.income.length" style="vertical-align : middle;text-align:center;">
                {{ item2.length }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.details }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.jan }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.feb }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.mar }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.apr }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.may }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.jun }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.jul }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.aug }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.sep }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.oct }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.nov }}</td>
                <td :class="[k != 'result' ? 'dark-gray': '']">{{ item2.dec }}</td>
                
               
              </tr>
            </tbody>
          </table>
        </div>
        <div class="col-md-4">
          <div class="form-group">
            <input type="submit" value="Create" class="btn btn-primary" />
          </div>
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
  async mounted() {},
  data() {
    return {
      selectedYear: null,
      sz:3,
      years: ["2018", "2019", "2020", "2021", "2022"],
      PredefinedIncomeCostList: [],
      ReconciliationList: [],
    };
  },
  methods: {
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
      if (response) {
        this.PredefinedIncomeCostList = response.list;
        this.cost = this.PredefinedIncomeCostList["cost"];
        this.cumulativeCost = this.PredefinedIncomeCostList["cumulativeCost"];
      }
    },
    async getReconciliationList() {
      let response = await BookkeepingService.getAllReconciliationsByYear(
        this.selectedYear
      );
      if (response) {
        this.ReconciliationList = response.list;
        console.log(this.ReconciliationList)
      }
    },
    async updateReconciliations(ReconciliationList) {
      let formData = new FormData();
      let userdata = ReconciliationList;

      Object.entries(userdata).forEach(([key, value]) => {
        if (value == null) {
          formData.append(key, "");
        } else {
          formData.append(key, value);
        }
      });
      let response = await BookkeepingService.Reconcile(formData);
      if (response) {
        this.$toast.success(res.data.message);
        this.getReconciliationList();
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