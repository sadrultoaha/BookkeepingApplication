<template>
  <div class="content">
    <div class="row col-md-10 justify-content-center">
      <h4>Income Entry Page</h4>
    </div>
    <div class="row" v-if="mode != 1">
      <div class="col-md-3">
        <label for="typeName" class="form-label">Income Type</label>
        <Select
          :searchable="true"
          :options="recordTypesList"
          v-model="predefinedRecords.typeId"
          type="text"
          autocomplete="off"
          class="form-control"
          id="typeName "
          trackBy="typeName"
          name="typeName"
          label="typeName"
          valueProp="id"
          placeholder="Select"
        />
      </div>
      <div class="col-md-3">
        <label for="date">Date</label>
        <Datepicker
          v-model="predefinedRecords.date"
          :lower-limit="new Date('01-01-2000')"
          inputFormat="dd-MM-yyyy"
          placeholder="dd-mm-yyyy"
          id="date"
          name="date"
        />
      </div>
      <div class="col-md-2">
        <label for="amount">Amount</label>
        <input
          autocomplete="off"
          type="number"
          id="amount"
          v-model="predefinedRecords.amount"
        />
      </div>
    </div>

    <br />
    <br />
    <br />
    <br />

    <div class="row">
      <div class="col-md-6" v-if="mode == 2">
        <button @click="enableView" type="button" class="btn btn-secondary">
          Back
        </button>
      </div>
      <div class="col-md-6" v-if="mode == 3">
        <button @click="enableView" type="button" class="btn btn-secondary">
          Back
        </button>
      </div>
      <div class="col-md-6" v-if="mode == 3">
        <button
          @click="updateAndEnableView"
          type="button"
          class="btn btn-warning"
        >
          Update
        </button>
      </div>
      <div class="col-md-6">
        <button
          v-if="mode == 2"
          @click="saveAndEnableView"
          type="button"
          class="btn btn-success"
        >
          Save
        </button>
      </div>
    </div>
    <div v-if="mode == 1" class="row col-md-3">
      <button @click="enableAdd" type="button" class="btn btn-primary">
        Add New
      </button>
    </div>

    <br /><br />
    <form class="row">
      <div class="col-md-6 form-group">
        <table class="table table-bordered">
          <thead>
            <tr style="background: #f4f6ff">
              <th scope="col">Serial No</th>
              <th scope="col">Type Name</th>
              <th scope="col">Date</th>
              <th scope="col">Amount</th>
              <th scope="col">Edit</th>
              <th scope="col">Delete</th>
            </tr>
          </thead>
          <tbody v-for="(list, index) in predefinedRecordsList" :key="index">
            <tr>
              <td>{{ index + 1 }}</td>
              <td>{{ list.typeName }}</td>
              <td>{{ list.date }}</td>
              <td>{{ list.amount }}</td>
              <td>
                <button
                  @click="enableEdit(list)"
                  type="button"
                  class="btn-active"
                ></button>
              </td>
              <td>
                <button
                  type="button"
                  class="btn-danger"
                  @click="deleteAndEnableView(list.id)"
                ></button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </form>
    <br />
  </div>
</template>

<script>
import Select from "@vueform/multiselect";
import Datepicker from "vue3-datepicker";
import moment from "moment";
import BookkeepingService from "../services/BookkeepingService";
export default {
  name: "Income",
  components: {
    Select,
    Datepicker,
    moment,
  },
  async mounted() {
    await this.getPredefinedRecordsList();
    await this.getRecordTypesList();
  },
  data() {
    return {
      mode: 1, //1=view 2=add 3=edit
      recordTypesList: [],
      predefinedRecordsList: {},
      predefinedRecords: {},
      actionNames: ["INCOME", "EXPENSE"],
    };
  },
  methods: {
    async getPredefinedRecordsList() {
      let response = await BookkeepingService.getAllPredefinedRecords();
      if (response) {
        this.predefinedRecordsList = response.list.filter(function (e) {
          return e.actionName.toUpperCase() == "INCOME";
        });
      }
    },
    async getRecordTypesList() {
      let response = await BookkeepingService.getAllRecordTypes();
      if (response) {
        this.recordTypesList = response.list.filter(function (e) {
          return e.actionName.toUpperCase() == "INCOME";
        });
      }
    },
    async saveAndEnableView() {
      let userdata = this.predefinedRecords;
      let jsonData = {};
      if (userdata.date) {
        userdata.date = moment(userdata.date).format("YYYY-MM-DD");
      }
      Object.entries(userdata).forEach(([key, value]) => {
        if (value == null) {
          jsonData[key] = "";
        } else if (value == "0001-01-01T00:00:00") {
          jsonData[key] = "";
        } else {
          jsonData[key] = value;
        }
      });
      
      let response = await BookkeepingService.addPredefinedRecord(jsonData);
      if (response) {
        await this.getPredefinedRecordsList();
        this.$toast.success(response.message);
        this.mode = 1;
      }
    },
    async updateAndEnableView() {
      let userdata = this.predefinedRecords;
      let jsonData = {};
      if (userdata.date) {
        userdata.date = moment(userdata.date).format("YYYY-MM-DD");
      }
      Object.entries(userdata).forEach(([key, value]) => {
        if (value == null) {
          jsonData[key] = "";
        } else if (value == "0001-01-01T00:00:00") {
          jsonData[key] = "";
        } else {
          jsonData[key] = value;
        }
      });
      
      let response = await BookkeepingService.updatePredefinedRecord(jsonData);
      if (response) {
        await this.getPredefinedRecordsList();
        this.$toast.success(response.message);
        this.mode = 1;
      }
    },
    async deleteAndEnableView(id) {
      let response = await BookkeepingService.deletePredefinedRecord(id);
      if (response) {
        await this.getPredefinedRecordsList();
        this.mode = 1;
      }
    },
    enableView() {
      this.mode = 1;
      this.predefinedRecords = {};
    },
    enableAdd() {
      this.predefinedRecords = {};
      this.mode = 2;
    },
    enableEdit(obj) {
      this.predefinedRecords.id = obj.id;
      this.predefinedRecords.typeId = obj.typeId;
      this.predefinedRecords.date = moment(obj.date).toDate();
      this.predefinedRecords.amount = obj.amount;
      this.mode = 3;
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

.table thead th,
.table-bordered td,
.table-bordered th {
  border-color: black;
}
.edit-color {
  background-color: #fff2cc;
}

table th {
  position: -webkit-sticky;
  position: sticky;
  top: 0;
  z-index: 1;
  background: #f4f6ff;
}
</style>