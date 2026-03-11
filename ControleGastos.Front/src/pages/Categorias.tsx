import { useEffect, useState } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import { Categoria, FinalidadeCategoria } from "../types";
import {
  listarCategorias,
  criarCategoria,
  atualizarCategoria,
  deletarCategoria
} from "../services/categoriaService";

function Categorias() {
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [descricao, setDescricao] = useState<string>("");
  const [finalidade, setFinalidade] = useState<FinalidadeCategoria>(FinalidadeCategoria.Despesa);

  const [modalAberto, setModalAberto] = useState(false);
  const [editandoId, setEditandoId] = useState<string | null>(null);
  const [descricaoEdit, setDescricaoEdit] = useState("");
  const [finalidadeEdit, setFinalidadeEdit] = useState<FinalidadeCategoria>(FinalidadeCategoria.Despesa);

  useEffect(() => {
    carregarCategorias();
  }, []);

  async function carregarCategorias() {
    try {
      const data = await listarCategorias();
      setCategorias(data);
    } catch (err) {
      console.error("Erro ao carregar categorias:", err);
      toast.error("Erro ao carregar categorias");
    }
  }

  async function salvar(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    if (!descricao) {
      toast.error("Preencha a descrição");
      return;
    }

    try {
      await criarCategoria({ descricao, finalidade });
      setDescricao("");
      setFinalidade(FinalidadeCategoria.Despesa);
      carregarCategorias();
      toast.success("Categoria criada com sucesso");
    } catch (err) {
      console.error(err);
      toast.error("Erro ao criar categoria");
    }
  }

  function abrirModal(categoria: Categoria) {
    setEditandoId(categoria.id);
    setDescricaoEdit(categoria.descricao);
    setFinalidadeEdit(categoria.finalidade);
    setModalAberto(true);
  }

  function fecharModal() {
    setModalAberto(false);
    setEditandoId(null);
  }

  async function atualizar() {
    if (!descricaoEdit || !finalidadeEdit) {
      toast.error("Preencha descrição e finalidade");
      return;
    }

    if (!editandoId) return;

    try {
      await atualizarCategoria(editandoId, {
        descricao: descricaoEdit,
        finalidade: finalidadeEdit
      });
      fecharModal();
      carregarCategorias();
      toast.success("Categoria atualizada com sucesso");
    } catch (err) {
      console.error(err);
      toast.error("Erro ao atualizar categoria");
    }
  }

  async function excluir(id: string) {
    const confirmar = window.confirm("Tem certeza que deseja excluir esta categoria?");
    if (!confirmar) return;

    try {
      await deletarCategoria(id);
      carregarCategorias();
      toast.success("Categoria deletada com sucesso");
    } catch (err) {
      console.error(err);
      toast.error("Erro ao deletar categoria");
    }
  }

  return (
    <div className="content">
      <h2>Categorias</h2>

      {/* Formulário criar */}
      <div className="form-card">
        <h3>Nova Categoria</h3>
        <form onSubmit={salvar}>
          <div className="form-group">
            <input
              placeholder="Descrição"
              value={descricao}
              onChange={(e) => setDescricao(e.target.value)}
            />
            <select
              value={finalidade}
              onChange={(e) =>
                setFinalidade(Number(e.target.value) as FinalidadeCategoria)
              }
            >
              <option value={FinalidadeCategoria.Despesa}>Despesa</option>
              <option value={FinalidadeCategoria.Receita}>Receita</option>
              <option value={FinalidadeCategoria.Ambas}>Ambas</option>
            </select>
            <button type="submit">Salvar</button>
          </div>
        </form>
      </div>

      {/* Tabela */}
      <table>
        <thead>
          <tr>
            <th>Descrição</th>
            <th>Finalidade</th>
            <th style={{ width: 200 }}>Ações</th>
          </tr>
        </thead>
        <tbody>
          {categorias.length > 0 ? (
            categorias.map((c) => (
              <tr key={c.id}>
                <td>{c.descricao}</td>
                <td>{FinalidadeCategoria[c.finalidade]}</td>
                <td>
                  <div className="actions">
                    <button className="btn-edit" onClick={() => abrirModal(c)}>Editar</button>
                    <button className="btn-delete" onClick={() => excluir(c.id)}>Excluir</button>
                  </div>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan={3} style={{ textAlign: "center", color: "#6b7280" }}>
                Nenhuma categoria cadastrada
              </td>
            </tr>
          )}
        </tbody>
      </table>

      {/* Modal editar */}
      {modalAberto && (
        <div className="modal-overlay">
          <div className="modal">
            <h3>Editar Categoria</h3>
            <input
              placeholder="Descrição"
              value={descricaoEdit}
              onChange={(e) => setDescricaoEdit(e.target.value)}
            />
            <select
              value={finalidadeEdit}
              onChange={(e) =>
                setFinalidadeEdit(Number(e.target.value) as FinalidadeCategoria)
              }
            >
              <option value={FinalidadeCategoria.Despesa}>Despesa</option>
              <option value={FinalidadeCategoria.Receita}>Receita</option>
              <option value={FinalidadeCategoria.Ambas}>Ambas</option>
            </select>
            <div className="actions">
              <button className="btn-edit" onClick={atualizar}>Atualizar</button>
              <button className="btn-delete" onClick={fecharModal}>Cancelar</button>
            </div>
          </div>
        </div>
      )}

      {/* Toastify Container */}
      <ToastContainer
        position="top-right"
        autoClose={3000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </div>
  );
}

export default Categorias;