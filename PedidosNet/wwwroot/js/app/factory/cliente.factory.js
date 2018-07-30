(function () {
    'use strict';

    angular.module('app').factory('clienteFactory', clienteFactory);

    clienteFactory.$inject = ['$http']; 

    function clienteFactory($http) {
        var service = {
            getCliente: getCliente
        };

        return service;

        function getCliente(cpf, senha) {
            var config = {
                params: {
                      cpf: cpf
                    , senha: senha
                }
            }
            return $http.get('/api/cliente/GetCliente', config);
        }
    }
})();