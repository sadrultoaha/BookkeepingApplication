import axiosInstance from "../http-bookkeepingapi.js";

class BookkeepingService {

    // PredfinedRecordsApis
    async getPredefinedRecordsByYear(year) {
        const response = await axiosInstance.get('PredefinedRecords/AllByYear', { params: { "year": year } }).then(
            response => {
                return response.data
            }
        ).catch(
            error => {
                return null
            }
        )
        return response
    }
    async getAllPredefinedRecords() {
        const response = await axiosInstance.get('PredefinedRecords/All').then(
            response => {
                return response.data
            }
        ).catch(
            error => {
                return null
            }
        )
        return response
    }
    async getPredefinedRecordById(id) {
        const response = await axiosInstance.get('PredefinedRecords/ById', { params: { "id": id } }).then(
            response => {
                return response.data
            }
        ).catch(
            error => {
                return null
            }
        )
        return response
    }
    async addPredefinedRecord(data) {
        const response = await axiosInstance.post('PredefinedRecords/Create', data).then(
            response => {
                return response.data
            }).catch(
                error => {
                    return null
                }
            )
        return response
    }
    async updatePredefinedRecord(data) {
        const response = await axiosInstance.patch('PredefinedRecords/Update', data).then(
            response => {
                return response.data
            }
        ).catch(
            error => {
                return null
            }
        )
        return response
    }
    async deletePredefinedRecord(id) {
        const response = await axiosInstance.delete('PredefinedRecords/Delete', { params: { "id": id } }).then(
            response => {
                return response.data
            }
        ).catch(
            error => {
                return null
            }
        )
        return response
    }


    // RecordTypesApis
    async getAllRecordTypes() {
        const response = await axiosInstance.get('RecordTypes/All').then(
            response => {
                return response.data
            }
        ).catch(
            error => {
                return null
            }
        )
        return response
    }
    async getActionWiseTypes() {
        const response = await axiosInstance.get('RecordTypes/GetActionWiseTypes').then(
            response => {
                return response.data
            }
        ).catch(
            error => {
                return null
            }
        )
        return response
    }
    async getRecordTypeById(id) {
        const response = await axiosInstance.get('RecordTypes/ById', { params: { "id": id } }).then(
            response => {
                return response.data
            }
        ).catch(
            error => {
                return null
            }
        )
        return response
    }
    async addRecordType(data) {
        const response = await axiosInstance.post('RecordTypes/Create', data).then(
            response => {
                return response.data
            }).catch(
                error => {
                    return null
                }
            )
        return response
    }
    async updateRecordType(data) {
        const response = await axiosInstance.patch('RecordTypes/Update', data).then(
            response => {
                return response.data
            }
        ).catch(
            error => {
                return null
            }
        )
        return response
    }
    async deleteRecordTypes(id) {
        const response = await axiosInstance.delete('RecordTypes/Delete', { params: { "id": id } }).then(
            response => {
                return response.data
            }
        ).catch(
            error => {
                return null
            }
        )
        return response
    }

    // ReconciliationRecordsApis
    async getAllReconciliationsByYear(year) {
        const response = await axiosInstance.get('ReconciliationRecords/AllByYear', { params: { "year": year } }).then(
            response => {
                return response.data
            }
        ).catch(
            error => {
                return null
            }
        )
        return response
    }
    async Reconcile(data) {
        const response = await axiosInstance.patch('ReconciliationRecords/Reconcile', data).then(
            response => {
                return response.data
            }
        ).catch(
            error => {
                return null
            }
        )
        return response
    }
  
}
export default new BookkeepingService();