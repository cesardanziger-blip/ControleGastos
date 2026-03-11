import { useEffect, useState, useRef } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import type { Transacao } from "../types";
import { listarTransacoes, criarTransacao } from "../services/transacaoService";
import { listarPessoas } from "../services/pessoaService";
import { listarCategorias } from "../services/categoriaService";
import { deletarTransacao } from "../services/transacaoService";

function Transacoes() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);

  // Campos do formulário
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState("");
  const [tipo, setTipo] = useState<1 | 2>(1);
  const [pessoa, setPessoa] = useState("");
  const [categoria, setCategoria] = useState("");

  // Listas de pessoas e categorias
  const [pessoasList, setPessoasList] = useState<{ id: string; nome: string; idade: number }[]>([]);
  const [categoriasList, setCategoriasList] = useState<
    { id: string; descricao: string; finalidade: 1 | 2 | 3 }[]
  >([]);

  const [loading, setLoading] = useState(true);
  const enviandoRef = useRef(false); // previne envio duplo

  useEffect(() => {
    carregarTransacoes();
    carregarPessoas();
    carregarCategorias();
  }, []);

  async function carregarTransacoes() {
    setLoading(true);
    try {
      const t = await listarTransacoes();
      setTransacoes(t);
    } finally {
      setLoading(false);
    }
  }

  async function carregarPessoas() {
    try {
      const p = await listarPessoas();
      setPessoasList(p);
    } catch (err) {
      toast.error("Erro ao carregar pessoas");
      console.error(err);
    }
  }

  async function carregarCategorias() {
    try {
      const c = await listarCategorias();
      setCategoriasList(c);
    } catch (err) {
      toast.error("Erro ao carregar categorias");
      console.error(err);
    }
  }

  async function salvar(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    if (enviandoRef.current) return;
    enviandoRef.current = true;

    // Validações
    if (!descricao) { toast.error("Informe uma descrição"); enviandoRef.current = false; return; }
    if (!valor || Number(valor) <= 0) { toast.error("O valor deve ser maior que zero"); enviandoRef.current = false; return; }
    if (!pessoa) { toast.error("Informe uma pessoa"); enviandoRef.current = false; return; }
    if (!categoria) { toast.error("Informe uma categoria"); enviandoRef.current = false; return; }

    const pessoaSelecionada = pessoasList.find((p) => p.id === pessoa);
    if (!pessoaSelecionada) { toast.error("Pessoa inválida"); enviandoRef.current = false; return; }

    if (tipo === 2 && pessoaSelecionada.idade < 18) {
      toast.error("Pessoa deve ter mais de 18 anos para cadastrar receita");
      enviandoRef.current = false;
      return;
    }

          console.log("descricao", descricao);
      console.log("valor", valor);
      console.log("tipo", tipo);
      console.log("pessoa", pessoa);
      console.log("categoria", categoria);

    try {
      await criarTransacao({
        descricao,
        valor: Number(valor),
        tipo,
        pessoa,
        categoria,
      });

      toast.success("Transação salva com sucesso");

      // Reset formulário
      setDescricao("");
      setValor("");
      setTipo(1);
      setPessoa("");
      setCategoria("");

      // Recarrega tabela
      carregarTransacoes();
    } catch (err: any) {
      const mensagem =
        err?.response?.data?.message ||
        err?.response?.data ||
        "Erro ao salvar transação";
      toast.error(mensagem);
      console.error(err);
    } finally {
      enviandoRef.current = false;
    }
  }

  // Helpers para exibir nome da pessoa e categoria na tabela
  function getNomePessoa(id: string) {
    return pessoasList.find((p) => p.id === id)?.nome || id;
  }

  function getNomeCategoria(id: string) {
    const cat = categoriasList.find((c) => c.id === id);
    if (!cat) return id;
    return `${cat.descricao} ${cat.finalidade === 1 ? "(Despesa)" : cat.finalidade === 2 ? "(Receita)" : "(Ambas)"
      }`;
  }

  return (
    <div className="content">
      <h2>Transações</h2>

      {/* Formulário */}
      <div className="form-card">
        <h3>Nova Transação</h3>
        <form onSubmit={salvar}>
          <div className="form-group">
            <input
              placeholder="Descrição"
              value={descricao}
              onChange={(e) => setDescricao(e.target.value)}
            />
            <input
              type="number"
              placeholder="Valor"
              value={valor}
              onChange={(e) => setValor(e.target.value)}
            />

            {/* Tipo */}
            <select
              value={tipo}
              onChange={(e) => setTipo(Number(e.target.value) as 1 | 2)}
              disabled={
                categoria
                  ? categoriasList.find((c) => c.id === categoria)?.finalidade !== 3
                  : true
              }
            >
              <option value={1}>Despesa</option>
              <option value={2}>Receita</option>
            </select>

            {/* Select Pessoa */}
            <select value={pessoa} onChange={(e) => setPessoa(e.target.value)}>
              <option value="">Selecione uma pessoa</option>
              {pessoasList.map((p) => (
                <option key={p.id} value={p.id}>
                  {p.nome} ({p.idade} anos)
                </option>
              ))}
            </select>

            {/* Select Categoria */}
            <select
              value={categoria}
              onChange={(e) => {
                const cat = categoriasList.find((c) => c.id === e.target.value);
                if (cat) {
                  setCategoria(cat.id);
                  if (cat.finalidade === 1) setTipo(1);
                  else if (cat.finalidade === 2) setTipo(2);
                  // Se for Ambas (3), tipo permanece editável
                }
              }}
            >
              <option value="">Selecione uma categoria</option>
              {categoriasList.map((c) => (
                <option key={c.id} value={c.id}>
                  {c.descricao} {c.finalidade === 1 ? "(Despesa)" : c.finalidade === 2 ? "(Receita)" : "(Ambas)"}
                </option>
              ))}
            </select>

            <button type="submit" disabled={enviandoRef.current}>
              {enviandoRef.current ? "Salvando..." : "Salvar"}
            </button>
          </div>
        </form>
      </div>

      {/* Tabela */}
      {loading ? (
        <div className="loading">Carregando dados...</div>
      ) : (
        <table>
          <thead>
            <tr>
              <th>Descrição</th>
              <th>Valor</th>
              <th>Tipo</th>
              <th>Pessoa</th>
              <th>Categoria</th>
            </tr>
          </thead>
          <tbody>
            {transacoes.length > 0 ? (
              transacoes.map((t) => (
                <tr key={t.id}>
                  <td>{t.descricao}</td>
                  <td
                    style={{
                      color: t.tipo === 1 ? "#dc2626" : "#16a34a",
                      fontWeight: 600,
                    }}
                  >
                    {t.tipo === 1 ? "-" : "+"} R$ {t.valor.toFixed(2)}
                  </td>
                  <td>{t.tipo === 1 ? "Despesa" : "Receita"}</td>
                  <td>{getNomePessoa(t.pessoa)}</td>
                  <td>{getNomeCategoria(t.categoria)}</td>
                  <td>
                    <button
                      className="btn-delete"
                      onClick={async () => {
                        const confirmar = window.confirm(
                          "Tem certeza que deseja excluir esta transação?"
                        );
                        if (!confirmar) return;

                        try {
                          await deletarTransacao(t.id);
                          toast.success("Transação deletada com sucesso");
                          carregarTransacoes();
                        } catch (err) {
                          console.error(err);
                          toast.error("Erro ao deletar transação");
                        }
                      }}
                    >
                      Excluir
                    </button>
                  </td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={6} style={{ textAlign: "center", color: "#6b7280" }}>
                  Nenhuma transação cadastrada
                </td>
              </tr>
            )}
          </tbody>
        </table>
      )}

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

export default Transacoes;