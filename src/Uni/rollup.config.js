import rollupTs from "@rollup/plugin-typescript";
import rollupNodeResolve from "@rollup/plugin-node-resolve";
import rollupCommonJs from "@rollup/plugin-commonjs";
import rollupTerser from "@rollup/plugin-terser";
import { globSync } from "glob";

export const debug = {
    input: globSync("./wwwroot/js/**/*[!.d].ts"),
    output: {
        chunkFileNames: "chunks/[name]-[hash].js",
        dir: "./wwwroot/js",
        sourcemap: true
    },
    plugins: [rollupCommonJs(), rollupNodeResolve(), rollupTs()],
    watch: {
        include: "./wwwroot/js/**/*[!.d].ts"
    }
};

export const release = {
    input: globSync("./wwwroot/js/**/*[!.d].ts"),
    output: {
        chunkFileNames: "chunks/[name]-[hash].js",
        dir: "./wwwroot/js",
        sourcemap: false
    },
    plugins: [rollupCommonJs(), rollupNodeResolve(), rollupTs(), rollupTerser()],
    watch: {
        include: "./wwwroot/js/**/*[!.d].ts"
    }
};

const config = process.env.BUNDLE_TYPE === "debug" ? debug : release;

export default config;