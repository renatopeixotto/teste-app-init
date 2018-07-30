(function () {
    'use strict';

    angular.module('app').controller('indexController', indexController); 

    indexController.$inject = ['$scope', '$cookies', 'clienteFactory', 'pedidoFactory', 'produtoFactory'];

    function indexController($scope, $cookies, clienteFactory, pedidoFactory, produtoFactory) {
        var vm = $scope;

        vm.abaProdutos = true;
        vm.abaMeusPedidos = false;
        vm.ProdutoConsulta = {};
        vm.Cliente = {};
        vm.qtdCarrinho = 0;
        vm.valorTotalCarrinho = 0;
        vm.buscarProduto = buscarProduto;
        vm.getProdutos = getProdutos;
        vm.getMeusPedidos = getMeusPedidos;
        vm.modalCarrinho = modalCarrinho;
        vm.modalAddProdutoCarrinho = modalAddProdutoCarrinho;
        vm.addProdutoCarrinho = addProdutoCarrinho;
        vm.insert = insert;
        vm.finalizarPedido = finalizarPedido;
        vm.deletePedidoProduto = deletePedidoProduto;

        getCliente();
        getProdutos();
        getPedidoProduto();

        function getCliente() {
            var cpf = "74583719100";
            var senha = "123465";

            clienteFactory.getCliente(cpf, senha).then(function (response) {
                vm.Cliente = response.data;
            });
        }

        function getProdutos() {
            vm.abaProdutos = true;
            vm.abaMeusPedidos = false;
            vm.ProdutoConsulta = {};

            produtoFactory.getData().then(function (response) {
                vm.lstProduto = response.data;
            });
        }

        function getMeusPedidos() {
            vm.abaProdutos = false;
            vm.abaMeusPedidos = true;

            pedidoFactory.getMeusPedidos(vm.Cliente.Id).then(function (response) {
                vm.lstMeusPedidos = response.data;
            });
        }

        function getPedidoProduto() {
            RemoveCookiePedidoEmAberto();

            pedidoFactory.getPedidoProduto(vm.Cliente.Id).then(function (response) {
                vm.lstPedidoProduto = response.data;
                vm.qtdCarrinho = vm.lstPedidoProduto.length;
                vm.valorTotalCarrinho = 0;

                if (vm.lstPedidoProduto.length > 0) {
                    var valorTotalCarrinho = 0;
                    var idPedido = vm.lstPedidoProduto[0].pedido.id;

                    PutCookiePedidoEmAberto(idPedido);

                    vm.lstPedidoProduto.forEach(function (pp) {
                        var valorTotalProduto = pp.quantidade * pp.valorUnitario;
                        valorTotalCarrinho = valorTotalCarrinho + valorTotalProduto;

                        pp.valorTotalProduto = valorTotalProduto;
                        vm.valorTotalCarrinho = valorTotalCarrinho;
                    });
                }

                //console.log(vm.lstPedidoProduto);
            });
        }

        function buscarProduto() {
            produtoFactory.getProdutos(vm.ProdutoConsulta.Descricao).then(function (response) {
                vm.lstProduto = response.data;
                //console.log(vm.lstProduto);
            });
        }

        function modalCarrinho(model) {

            $('#modal-carrinho').modal();
        }

        function modalAddProdutoCarrinho(model) {
            vm.PedidoProduto = {};
            vm.PedidoProduto.Produto = angular.copy(model);
            $('#modal-add-produto-carrinho').modal();
        }

        function addProdutoCarrinho(model) {
            var existe = false;
            var idPedidoAberto = ($cookies.get('pedidoEmAberto') != undefined) ?  $cookies.get('pedidoEmAberto') : 0;
            var objPedidoProduto = {
                Pedido: { Id: idPedidoAberto, Cliente: vm.Cliente, Efetuado: false },
                Produto: model.Produto,
                Quantidade: model.quantidade
            };

            if (idPedidoAberto > 0) {
                vm.lstPedidoProduto.forEach(function (pp) {

                    if (pp.produto.id == model.Produto.id) {
                        existe = true;
                    }
                });
            }

            if (existe == false) {
                pedidoFactory.insertPedidoProduto(objPedidoProduto).then(function (response) {
                    getPedidoProduto();

                    $('#modal-add-produto-carrinho').modal('toggle');
                    $('#modal-carrinho').modal();
                    delete vm.PedidoProduto;
                });
            }
            else {
                alert('Produto já adicionado ao carrinho!');
                $('#modal-add-produto-carrinho').modal('toggle');
            }

        }

        function insert() {

            pedidoFactory.insert(vm.Pedido).then(function (response) {
                getData();
            });

            $('#modal-inserir-alterar').modal('toggle');
            delete vm.Pedido;
        }

        function finalizarPedido() {
            var idPedidoAberto = ($cookies.get('pedidoEmAberto') != undefined) ? $cookies.get('pedidoEmAberto') : 0;

            if (idPedidoAberto > 0) {

                var pedido = { Id: idPedidoAberto };

                pedidoFactory.finalizarPedido(pedido).then(function (response) {
                    alert('Pedido Finalizado com Sucesso!');
                    $('#modal-carrinho').modal('toggle');
                    getPedidoProduto();
                });

                $('#modal-inserir-alterar').modal('toggle');
                RemoveCookiePedidoEmAberto();
            }
            else {
                alert('Não há pedido para finalizar!')
            }
        }

        function deletePedidoProduto(model) {
            pedidoFactory.deletePedidoProduto(model.id).then(function (response) {
                getPedidoProduto();
            });
        }

        function PutCookiePedidoEmAberto(idPedido) {
            $cookies.put('pedidoEmAberto', idPedido, { path: "/" });
        }

        function RemoveCookiePedidoEmAberto() {
            $cookies.remove('pedidoEmAberto');
        }


    }
})();
