<script>
    import InputComponent from "./InputComponent.svelte";
    import axios from "axios";

    let linkText = "";
    
    function onChangeInput(e) {
        linkText = e.target.value;
    }

    async function download() {
        let res = await axios.post("http://localhost:5000/DOWNLOAD", { link: linkText });
    }
</script>

<style>

</style>

<div class="download-menu">
    <p style="color:white">Dowload</p>
    <InputComponent onChange="{onChangeInput}"/>
    <button on:click={download}>Download</button>
</div>