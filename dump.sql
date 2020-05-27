--
-- PostgreSQL database dump
--

-- Dumped from database version 12.3
-- Dumped by pg_dump version 12.2

-- Started on 2020-05-26 21:17:16

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 203 (class 1259 OID 16404)
-- Name: fabricantes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.fabricantes (
    codfabricante integer NOT NULL,
    nome character varying(25)
);


ALTER TABLE public.fabricantes OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 16439)
-- Name: imagens; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.imagens (
    codimagem integer NOT NULL,
    codveiculo integer NOT NULL,
    url character varying(1000) NOT NULL
);


ALTER TABLE public.imagens OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 16411)
-- Name: modelos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.modelos (
    codmodelo integer NOT NULL,
    codfabricante integer NOT NULL,
    nome character varying NOT NULL
);


ALTER TABLE public.modelos OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 16394)
-- Name: usuarios; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usuarios (
    codusuario integer NOT NULL,
    email character varying(100) NOT NULL,
    senha bytea NOT NULL,
    chave bytea NOT NULL,
    nome character varying(50) NOT NULL
);


ALTER TABLE public.usuarios OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 16426)
-- Name: veiculos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.veiculos (
    codveiculo integer NOT NULL,
    codmodelo integer NOT NULL,
    ano character varying NOT NULL,
    km integer NOT NULL,
    valor numeric NOT NULL,
    descricao character varying(500),
    observacao character varying(500),
    cor character varying(15) NOT NULL
);


ALTER TABLE public.veiculos OWNER TO postgres;

--
-- TOC entry 2710 (class 2606 OID 16410)
-- Name: fabricantes fabricantes_nome_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fabricantes
    ADD CONSTRAINT fabricantes_nome_key UNIQUE (nome);


--
-- TOC entry 2712 (class 2606 OID 16408)
-- Name: fabricantes fabricantes_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fabricantes
    ADD CONSTRAINT fabricantes_pkey PRIMARY KEY (codfabricante);


--
-- TOC entry 2720 (class 2606 OID 16446)
-- Name: imagens imagens_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.imagens
    ADD CONSTRAINT imagens_pkey PRIMARY KEY (codimagem);


--
-- TOC entry 2714 (class 2606 OID 16420)
-- Name: modelos modelos_nome_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.modelos
    ADD CONSTRAINT modelos_nome_key UNIQUE (nome);


--
-- TOC entry 2716 (class 2606 OID 16418)
-- Name: modelos modelos_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.modelos
    ADD CONSTRAINT modelos_pkey PRIMARY KEY (codmodelo);


--
-- TOC entry 2706 (class 2606 OID 16403)
-- Name: usuarios usuarios_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuarios
    ADD CONSTRAINT usuarios_email_key UNIQUE (email);


--
-- TOC entry 2708 (class 2606 OID 16401)
-- Name: usuarios usuarios_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuarios
    ADD CONSTRAINT usuarios_pkey PRIMARY KEY (codusuario);


--
-- TOC entry 2718 (class 2606 OID 16433)
-- Name: veiculos veiculos_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.veiculos
    ADD CONSTRAINT veiculos_pkey PRIMARY KEY (codveiculo);


--
-- TOC entry 2723 (class 2606 OID 16447)
-- Name: imagens imagens_codveiculo_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.imagens
    ADD CONSTRAINT imagens_codveiculo_fkey FOREIGN KEY (codveiculo) REFERENCES public.veiculos(codveiculo);


--
-- TOC entry 2721 (class 2606 OID 16421)
-- Name: modelos modelos_codfabricante_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.modelos
    ADD CONSTRAINT modelos_codfabricante_fkey FOREIGN KEY (codfabricante) REFERENCES public.fabricantes(codfabricante) NOT VALID;


--
-- TOC entry 2722 (class 2606 OID 16434)
-- Name: veiculos veiculos_codmodelo_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.veiculos
    ADD CONSTRAINT veiculos_codmodelo_fkey FOREIGN KEY (codmodelo) REFERENCES public.modelos(codmodelo);


-- Completed on 2020-05-26 21:17:16

--
-- PostgreSQL database dump complete
--

