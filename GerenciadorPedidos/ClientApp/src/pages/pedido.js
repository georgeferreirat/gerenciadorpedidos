import React, { useState, useEffect, useContext } from 'react';
import { useAccordionButton } from 'react-bootstrap/AccordionButton';
import Table from 'react-bootstrap/Table';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import Spinner from 'react-bootstrap/Spinner';
import Card from 'react-bootstrap/Card';
import Accordion from 'react-bootstrap/Accordion';
import AccordionContext from "react-bootstrap/AccordionContext";
import Image from 'react-bootstrap/Image'
import carrinhocompras from '../assets/icon/carrinho-de-compras.png';

function ContextAwareToggle({ children, eventKey, callback }) {
  const { activeEventKey } = useContext(AccordionContext);

  const decoratedOnClick = useAccordionButton(
    eventKey,
    () => callback && callback(eventKey),
  );

  const isCurrentEventKey = activeEventKey === eventKey;
console.log(children, eventKey, callback, "ContextAwareToggle")
  return (
    <button
      type="button"
      style={{ backgroundColor: isCurrentEventKey ? 'pink' : 'lavender' }}
      onClick={(e) =>decoratedOnClick()}

    >
      {children}
    </button>
  );
}

const Pedido = () => {
    const [loading, setloading] = useState(false);
    const [alerting, setAlerting] = useState(false);
    const [formulario, setFormulario] = useState(true);
    
    
    const [typeSend, setTypeSend] = useState([]);  
    const [title, setTitle] = useState([]);
    const [preco, setPreco] = useState([]);
    const [descricao, setDescricao] = useState([]);
    const [pedido, setPedido] = useState([]);
    const [fornecedor, setFornecedor] = useState([]);
    const [show, setShow] = useState(false);
    const [showdelete, setShowdelete] = useState(false);
    
    useEffect(() => {
      getall();
    }, [])

    const fnHandleSubmit = event => {
      event.preventDefault();
      // iniciar requisição 
      setloading(true);
      setFormulario(false);

      let pedido = {
        "descricao": event.target[0].value,
        "preco": event.target[1].value,
        "fornecedor": null,
        "produtos": null
      }
      submit(pedido);
  }

    function getall() {
        const requestOptions = {
        };
        fetch("https://localhost:7228/api/Pedido", requestOptions)
            .then(res => res.json())
            .then(
                (data) => {
                  setPedido(data);
                },
                (error) => {

                }
            ).finally(() => {
             
            })
    }

    function editar(id) {
      setFormulario(true);
      console.log('Pedidos, editar', id);
      fetch("https://localhost:7228/api/Pedido/"+id)
      .then(res => res.json())
      .then(
          (data) => {
              console.log(data.fornecedor.id)
              setTitle("Editar Pedido");
              setPreco(data.preco);
              setDescricao(data.descricao);
              setFornecedor(data.fornecedor.id);
              setShow(true);

              setTypeSend({type:"update", id: id});
              
          },
          (error) => {

          }
      )
    }


    function getPedidoListagensProdutos(id) {
      setFormulario(true);
      //TODO::criar no back-end a consulta dos pedidos
      fetch("https://localhost:7228/api/Pedido/"+id)
      .then(res => res.json())
      .then(
          (data) => {
              setTitle("Editar  Produtos");
              setPreco(data.preco);
              setDescricao(data.descricao);
              setFornecedor(data.fornecedor.id);
              setShow(true);

              setTypeSend({type:"update", id: id});
              
          },
          (error) => {

          }
      )
    }

    function deletar(id) {
      console.log('Pedidos, deletar', id);
      setTypeSend({type:"delete", id: id});

      handleShowDelete();
    }

    function criar() {
      setFormulario(true);
      setShow(true);
      setPreco();
      setDescricao("");
      setTitle("Criar Pedido");
      setTypeSend({type:"create", id: 0});
      console.log('Pedidos, Criar');
    }
    
    const createClose = () => setShow(false);

    function update(id, pedido) {
      const requestOptions = {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(pedido)
      };
      fetch('https://localhost:7228/api/Pedido/'+id, requestOptions)
        .then(response => response.json())
          .then(data => console.log(data) )
        .finally(() => {
          getall()
          setloading(false);
          setAlerting(true);
          setFormulario(false);
        });
    }

    function deletepedido(id) {
      const requestOptions = {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
      };
      fetch('https://localhost:7228/api/Pedido/'+id, requestOptions)
        .then(response => response.json())
        .then(data => console.log("update - pedido") )
        .finally(() => {
          getall()
          setloading(false);          
          setAlerting(false);
          handleCloseDelete();
        });
    }


    function create(pedido) {
      const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(pedido)
      };
      fetch('https://localhost:7228/api/Pedido/', requestOptions)
        .then(response => response.json())
        .then(data => console.log("update - pedido") )
        .finally(() => {
          getall()
          setloading(false);
          setAlerting(true);
          setFormulario(false);
        });
    }


    function submit(pedido) {
        console.log(pedido)
        switch (typeSend.type) {
        case "create":
          create(pedido);
          break;
        case "update":
          update(typeSend.id, pedido);
          break;
        case "delete":
          deletepedido(typeSend.id);
          break;
          
        default:
          break;
      }
    }
    
  const handleCloseDelete = () => setShowdelete(false);
  const handleShowDelete = () => setShowdelete(true);

return(
  <>
    

    <Container>
      <Row xs={2} md={4} lg={6}>
      <Col>
        <h1>
          Pedidos {' '} <Button variant="success"  onClick={criar} size="lg">Criar</Button>
        </h1>
        </Col>  {' '}
      </Row>
      {' '}
      {/* add pedido */}
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>#</th>
            <th>Descrição</th>
            <th>Qtd Produtos</th>
            <th>Preço</th>
            <th>Nome do Fornecedor</th>
            <th>Data de Cadastro</th>
            <th>Ver Carrinho</th>
            <th>Editar / Deletar</th>
          </tr>
        </thead>
      
        <tbody>
          {pedido.map(data => (
              <tr key={data.id}>
                <td>
                  {data.id}
                </td>
                <td>
                  {data.descricao}
                </td>
                <td>
                  {data.qtdProduto}
                </td>
                <td>
                  {data.preco}
                </td>
                <td>
                  {data.fornecedor.nome}
                </td>
                <td>
                  {data.dataCadastro}
                </td>
                <td>
                  <Button variant="primary" onClick={(e) =>editar(data.id)} size="lg">Produtos</Button>
                </td>
                <th>
                  <Button variant="primary" onClick={(e) =>editar(data.id)} size="lg">Editar</Button>
                  {' '}
                  <Button variant="danger" onClick={(e) =>deletar(data.id)} size="lg">Deletar</Button>
                  {' '}
                </th>
              </tr>

              
          ))}        
        </tbody>
      </Table>
      {/* MODAL */}
      <Modal show={show} onHide={createClose} animation={false} fade={"false"}>

        <Modal.Header closeButton>
          <Modal.Title>{title}</Modal.Title>
        </Modal.Header>
        <Spinner animation="border" role="status" style={{display: loading ? 'block' : 'none' }}>
          <span className="visually-hidden">Loading...</span>
        </Spinner>
        <Container style={{display: alerting ? 'block' : 'none' }}>
          <div >
            Cadastrado com Sucesso!
            <hr />
            <div className="d-flex justify-content-end">
              <Button onClick={createClose} variant="outline-success">
                Ok
              </Button>
            </div>
          </div>
        </Container>
        <Container style={{display: formulario ? 'block' : 'none' }}>
          <form onSubmit={fnHandleSubmit}>
            <Form.Group className="mb-3" controlId="formBasicDescricao">
              <Form.Label>Descrição</Form.Label>
              <Form.Control name="descricao" type="text" placeholder="Enter descricao" defaultValue={descricao} />
              <Form.Text className="text-muted">
               Descrição do Pedido
              </Form.Text>
            </Form.Group>

            <Form.Group className="mb-3" controlId="formBasicPreco">
              <Form.Label>Preço</Form.Label>
              <Form.Control type="number" placeholder="Enter preco" defaultValue={preco} />
              <Form.Text className="text-muted">
               Descrição do Preço
              </Form.Text>
              </Form.Group>
          
            <Modal.Footer>
              <Button variant="secondary" onClick={createClose}>
                Fechar
              </Button>
              
              <Button variant="primary" type="submit" >
                Salvar
              </Button>
            </Modal.Footer>
          </form>
        </Container>
      </Modal>
      {/* Delete */}

      <Modal show={showdelete} onHide={handleCloseDelete} animation={false} fade={"false"}>
        <Modal.Header closeButton>
          <Modal.Title>Tem certeza que deseja deletar o Pedido</Modal.Title>
        </Modal.Header>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseDelete}>
            Cancelar
          </Button>
          <Button variant="danger" onClick={submit} >
            Deletar
          </Button>
        </Modal.Footer>
      </Modal>

    </Container>
  </>
);
}
export default Pedido;