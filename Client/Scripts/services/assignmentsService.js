angular.module('deviceManagementApp')
    .service('assignmentsService', ['$http', 'requestService',
        function ($http, requestService) {
            this.getDevices = function () {
                return requestService.get('/GetAllDevices');
            }

            this.getManufacturers = function () {
                return requestService.get('/GetAllManufacturers');
            }

            this.getTypes = function () {
                return requestService.get('/GetAllDeviceTypes');
            }

            this.getOperatingSystems = function () {
                return requestService.get('/GetAllOS');
            }

            this.getProcessors = function () {
                return requestService.get('/GetAllProcessors');
            }

            this.getUserById = function (userId) {
                return requestService.get('/GetOneUser?id=' + userId);
            }

            this.getUserForDevice = function (id) {
                return requestService.get('/GetUserForDevice?id=' + id);
            }

            this.assign = function (idDevice, idUser) {
                var data = {
                    device: idDevice,
                    user: idUser
                }

                return requestService.post('/Assign', data);
            }

            this.unassign = function (idDevice, idUser) {
                var data = {
                    device: idDevice,
                    user: idUser
                }

                return requestService.put('/Unassign', data);
            }

            this.deleteDevice = function (idDevice) {
                return requestService.delete('/DeleteDevice?id=' + idDevice);
            }

            this.insertDevice = function (name, manufacturer, type, OS, processor, RAM){
                var data = {
                    name: name,
                    manufacturer: manufacturer.Id,
                    type: type.Id,
                    OS: OS.Id,
                    processor: processor.Id,
                    RAM: RAM
                }
                return requestService.post('/InsertDevice', data);
            }

            this.updateDevice = function (id, name, manufacturer, type, OS, processor, RAM) {
                var data = {
                    id: id,
                    name: name,
                    manufacturer: manufacturer.Id,
                    type: type.Id,
                    OS: OS.Id,
                    processor: processor.Id,
                    RAM: RAM
                }

                return requestService.put('/UpdateDevice', data);
            }

        }
    ]);