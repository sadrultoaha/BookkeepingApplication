<template>
  <div class="content">
    <div class="row col-md-10 justify-content-center">
      <h4>Types Entry Page</h4>
    </div>
    <div class="row" v-if="mode != 1">
      <div class="col-md-3">
        <label for="actionName" class="form-label">Action Type</label>
        <Select
          :searchable="true"
          :options="actionNames"
          v-model="recordTypes.actionName"
          type="text"
          autocomplete="off"
          class="form-control"
          placeholder="Select"
        />
      </div>
      <div class="col-md-2">
        <label for="typeName">Type Name</label>
        <input
          autocomplete="off"
          type="text"
          id="typeName"
          v-model="recordTypes.typeName"
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
              <th scope="col">Action Name</th>
              <th scope="col">Type Name</th>
              <th scope="col">Edit</th>
              <th scope="col">Delete</th>
            </tr>
          </thead>
          <tbody v-for="(list, index) in recordTypesList" :key="index">
            <tr>
              <td>{{ index + 1 }}</td>
              <td>{{ list.actionName }}</td>
              <td>{{ list.typeName }}</td>
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
  name: "Types",
  components: {
    Select,
    Datepicker,
    moment,
  },
  async mounted() {
    await this.getRecordTypesList();
  },
  data() {
    return {
      mode: 1, //1=view 2=add 3=edit
      recordTypesList: [],
      recordTypes: {},
      actionNames: ["INCOME", "EXPENSE"],
    };
  },
  methods: {
    async getRecordTypesList() {
      let response = await BookkeepingService.getAllRecordTypes();
      if (response) {
        this.recordTypesList = response.list;
      }
    },
    async saveAndEnableView() {
      let userdata = this.recordTypes;
      let jsonData = {};

      Object.entries(userdata).forEach(([key, value]) => {
        if (value == null) {
          jsonData[key] = "";
        } else {
          jsonData[key] = value;
        }
      });
      let response = await BookkeepingService.addRecordType(jsonData);
      if (response) {
        await this.getRecordTypesList();
        this.$toast.success(response.message);
        this.mode = 1;
      }
    },
    async updateAndEnableView() {
      let userdata = this.recordTypes;
      let jsonData = {};

      Object.entries(userdata).forEach(([key, value]) => {
        if (value == null) {
          jsonData[key] = "";
        } else {
          jsonData[key] = value;
        }
      });

      let response = await BookkeepingService.updateRecordType(jsonData);
      if (response) {
        await this.getRecordTypesList();
        this.$toast.success(response.message);
        this.mode = 1;
      }
    },
    async deleteAndEnableView(id) {
      let response = await BookkeepingService.deleteRecordTypes(id);
      if (response) {
        await this.getRecordTypesList();
        this.mode = 1;
      }
    },
    enableView() {
      this.mode = 1;
      this.recordTypes = {};
    },
    enableAdd() {
      this.recordTypes = {};
      this.mode = 2;
    },
    enableEdit(obj) {
      this.recordTypes.id = obj.id;
      this.recordTypes.actionName = obj.actionName;
      this.recordTypes.typeName = obj.typeName;
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