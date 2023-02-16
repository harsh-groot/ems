import axios from "axios";

export const deleteApiService = async (url, config) => {
   return await axios.delete(url, config)
        .then((res) => {
            return res
        })
       .catch((err) => {
            return err
        })
}