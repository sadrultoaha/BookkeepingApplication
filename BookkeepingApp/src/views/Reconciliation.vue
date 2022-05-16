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
                <span clas = "justify-content-center">Year {{ this.selectedYear }} </span>
              </tr>
            </thead>
            <tbody>
              <tr>
                <th scope="col"></th>
                <th scope="col"></th>
                <th scope="col">Jan</th>
                <th scope="col">Feb</th>
                <th scope="col">Mar</th>
                <th scope="col">Apr</th>
                <th scope="col">May</th>
                <th scope="col">Jun</th>
                <th scope="col">Jul</th>
                <th scope="col">Aug</th>
                <th scope="col">Sep</th>
                <th scope="col">Oct</th>
                <th scope="col">Nov</th>
                <th scope="col">Dec</th>
              </tr>
              <tr v-for="(action, k1) in PredefinedIncomeCostList" :key="k1">
                  <td></td>
                  <td>{{ action.details }}</td>
                  <td>{{ action.jan }}</td>
                  <td>{{ action.feb }}</td>
                  <td>{{ action.mar }}</td>
                  <td>{{ action.apr }}</td>
                  <td>{{ action.may }}</td>
                  <td>{{ action.jun }}</td>
                  <td>{{ action.jul }}</td>
                  <td>{{ action.aug }}</td>
                  <td>{{ action.sep }}</td>
                  <td>{{ action.oct }}</td>
                  <td>{{ action.nov }}</td>
                  <td>{{ action.dec }}</td>

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
      years: ["2018", "2019", "2020", "2021", "2022"],
      cost: {},
      cumulativeCost: {},
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
</style>