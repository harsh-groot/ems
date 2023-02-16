import axios from "axios";

export const getApiService = async (url, config) => {
  return await axios.get(url, config)
        .then((res) => {
            return res?.data?.responseData
        })
      .catch((err) => {
            return err
        })
}