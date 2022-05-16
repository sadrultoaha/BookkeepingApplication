<template>
  <div style="width: 150%">
    <form class="row">
      <div class="form-group">
        <table class="table table-bordered table-responsive">
          <thead>
            hello
          </thead>
          <tbody>
            guy
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
    await this.getIncomCostList();
    await this.getReconciliationList();
  },
  data() {
    return {
      selectedYear:2020,
      years: ["2018", "2019", "2020", "2021", "2022"],
      PredefinedIncomeCostList: [],
      ReconciliationList: [],
    };
  },
  methods: {
    async getIncomCostList() {
      let response = await BookkeepingService.getPredefinedRecordsByYear(this.selectedYear);
      if (response) {
        this.PredefinedIncomeCostList = response.data.list;
      }
    },
    async getReconciliationList() {
      let response = await BookkeepingService.getAllReconciliationsByYear(this.selectedYear);
      if (response) {
        this.PredefinedIncomeCostList = response.data.list;
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