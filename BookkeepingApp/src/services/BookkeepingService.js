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
    async addPredefinedRecord(formData) {
        const response = await axiosInstance.post('PredefinedRecords/Create', formData).then(
            response => {
                return response.data
            }).catch(
                error => {
                    return null
                }
            )
        return response
    }
    async updatePredefinedRecord(formData) {
        const response = await axiosInstance.patch('PredefinedRecords/Update', formData).then(
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
        const response = await axiosInstance.patch('PredefinedRecords/Delete', { params: { "id": id } }).then(
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
    async addRecordType(formData) {
        const response = await axiosInstance.post('RecordTypes/Create', formData).then(
            response => {
                return response.data
            }).catch(
                error => {
                    return null
                }
            )
        return response
    }
    async updateRecordType(formData) {
        const response = await axiosInstance.patch('RecordTypes/Update', formData).then(
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
        const response = await axiosInstance.patch('RecordTypes/Delete', { params: { "id": id } }).then(
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
    async Reconcile(formData) {
        const response = await axiosInstance.patch('ReconciliationRecords/Reconcile', formData).then(
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