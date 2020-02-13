app.controller('assignmentsController', ['$scope', '$rootScope', '$location', '$animate', '$route', 'assignmentsService',
    function ($scope, $rootScope, $location, $animate, $route, assignmentsService) {
        $scope.Name = '';
        $scope.userId = localStorage.getItem("userId");
        $scope.isCollapsed = true;
        $scope.devices = [];
        $scope.manufacturers = [];
        $scope.types = [];
        $scope.operatingSystems = [];
        $scope.processors = [];
        $scope.isUpdate = false;
        $scope.isInsert = false;
        $scope.operation = '';
        $scope.selectedDevice;

        var getName = function () {
            assignmentsService.getUserById($scope.userId).then(function (r) {
                if (r.data.GetOneUserResult != null)
                    $scope.Name = r.data.GetOneUserResult.Name;
            });
        }
        getName();

        var getAllDevices = function () {
            assignmentsService.getDevices().then(function (r) {
                if (r.data.GetAllDevicesResult != null) {
                    var devicesList = r.data.GetAllDevicesResult;
                    angular.forEach(devicesList, function (device) {
                        var pair = [];
                        pair.push(device);
                        getUserForDevice(device.Id, pair);
                    });
                }
            });
        }
        getAllDevices();

        var getAllManufacturers = function () {
            assignmentsService.getManufacturers().then(function (r) {
                if (r.data.GetAllManufacturersResult != null) {
                    var manufacturersList = r.data.GetAllManufacturersResult;
                    angular.forEach(manufacturersList, function (value) {
                        $scope.manufacturers.push(value);
                    });
                }
            });
        }
        getAllManufacturers();

        var getAllTypes = function () {
            assignmentsService.getTypes().then(function (r) {
                if (r.data.GetAllTypesResult != null) {
                    var typesList = r.data.GetAllTypesResult;
                    angular.forEach(typesList, function (type) {
                        $scope.types.push(type);
                    });
                }
            });
        }
        getAllTypes();

        var getAllOperatingSystems = function () {
            assignmentsService.getOperatingSystems().then(function (r) {
                if (r.data.GetAllOSResult != null) {
                    var OSList = r.data.GetAllOSResult;
                    angular.forEach(OSList, function (OS) {
                        $scope.operatingSystems.push(OS);
                    });
                }
            });
        }
        getAllOperatingSystems();

        var getAllProcessors = function () {
            assignmentsService.getProcessors().then(function (r) {
                if (r.data.GetAllProcessorsResult != null) {
                    var processorsList = r.data.GetAllProcessorsResult;
                    angular.forEach(processorsList, function (processor) {
                        $scope.processors.push(processor);
                    });
                }
            });
        }
        getAllProcessors();

        var getUserForDevice = function (id, pair) {
            assignmentsService.getUserForDevice(id).then(function (r) {
                if (r.data.GetUserForDeviceResult != null) {
                    $scope.userName = r.data.GetUserForDeviceResult.Name;
                    pair.push($scope.userName);
                    pair.push(r.data.GetUserForDeviceResult);
                    $scope.devices.push(pair);
                }
                else {
                    $scope.userName = '';
                    pair.push($scope.userName);
                    $scope.devices.push(pair);
                }
            });
        }

        $scope.getOneFromList = function (id, list) {
            var result;
            angular.forEach(list, function (item) {
                if (item.Id == id) {
                    result = item;
                }
            });
            return result;
        }

        $scope.canAssign = function (index) {
            return $scope.devices[index][1] == '';
        }

        $scope.assign = function (id) {
            assignmentsService.assign(id, $scope.userId).then(function (r) {
                if (r.data.AssignResult) {
                    $route.reload();
                }
                else {
                    alert('A aparut o eroare!');
                }
            });
        }

        $scope.canUnassign = function (index) {
            if (!$scope.devices[index][2])
                return false;
            return $scope.devices[index][2].Id == $scope.userId;
        }

        $scope.unassign = function (id) {
            assignmentsService.unassign(id, $scope.userId).then(function (r) {
                console.log(r);
                if (r.data.UnassignResult) {
                    $route.reload();
                }
                else {
                    alert('A aparut o eroare!');
                }
            });
        }

        $scope.canDelete = function (user) {
            return user == '';
        }

        $scope.delete = function (id) {
            assignmentsService.deleteDevice(id).then(function (r) {
                if (r.data.DeleteDeviceResult) {
                    $route.reload();
                }
                else {
                    alert('A aparut o eroare!');
                }
            });
        }

        $scope.insertDevice = function () {
            $scope.operation = 'Insert';
            $scope.isInsert = !$scope.isInsert;
            $scope.selectedManufacturer = $scope.manufacturers[0];
            $scope.selectedType = $scope.types[0];
            $scope.selectedOS = $scope.operatingSystems[0];
            $scope.selectedProcessor = $scope.processors[0]
        }

        $scope.insert = function () {
            if ($scope.name = '' || $scope.ram <= 0) {
                shake();
            }
            else {
                assignmentsService.insertDevice($scope.deviceName, $scope.selectedManufacturer, $scope.selectedType, $scope.selectedOS, $scope.selectedProcessor, $scope.ram).then(function (r) {
                    if (r.data.InsertDeviceResult) {
                        $route.reload();
                    }
                    else {
                        alert('A aparut o eroare!');
                    }
                });
            }
        }

        $scope.updateDevice = function (id) {
            $scope.operation = 'Update';
            $scope.isUpdate = !$scope.isUpdate;

            var device;
            angular.forEach($scope.devices, function (item) {
                if (item[0].Id == id) {
                    device = item[0];
                }
            });

            $scope.selectedDevice = device;
            $scope.deviceName = device.Name;
            $scope.selectedManufacturer = $scope.getOneFromList(device.Manufacturer, $scope.manufacturers);
            $scope.selectedType = $scope.getOneFromList(device.Type, $scope.types);
            $scope.selectedOS = $scope.getOneFromList(device.OS, $scope.operatingSystems);
            $scope.selectedProcessor = $scope.getOneFromList(device.Processor, $scope.processors);
            $scope.ram = device.RAM;
        }

        $scope.update = function () {
            if ($scope.name = '' || $scope.ram <= 0) {
                shake();
            }
            else {
                assignmentsService.updateDevice($scope.selectedDevice.Id, $scope.deviceName, $scope.selectedManufacturer, $scope.selectedType, $scope.selectedOS, $scope.selectedProcessor, $scope.ram).then(function (r) {
                    if (r.data.UpdateDeviceResult) {
                        $route.reload();
                    }
                    else {
                        alert('A aparut o eroare!');
                    }
                });
            }
        }

        $scope.logout = function () {
            $location.path('/');
            localStorage.removeItem("userId");
        }

        var shake = function () {
            console.log('am intrat');
            var containerLogin = angular.element(document.querySelector('#container'));

            $animate.addClass(containerLogin, 'shake').then(function () {
                $animate.removeClass(containerLogin, 'shake');
            });

            $scope.wrongData = true;
        }
    }
]);