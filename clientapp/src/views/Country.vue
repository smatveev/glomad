<template>
    <!-- <h1>{{country.name}}</h1> -->
    <h1>{{$route.params.id}}</h1>

    <h1>{{country}}</h1>
</template>

<script>
export default ({
    //props: ['id'],
    data() {
        return {
            country: null,
            id: this.$route.params.id
        }
    },
    mounted() {
        fetch("https://localhost:5001/Countries/GetById?id=" + this.id)
        .then(res => res.json())
        .then(data => this.country = data)
        .catch(err => console.log(err.message))
    }
})
</script>
