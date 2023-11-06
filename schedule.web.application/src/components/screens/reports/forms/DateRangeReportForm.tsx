import {Button, Card, Form, FormGroup, Row} from "react-bootstrap";
import {useInfinitySelect, usePaginationQuery} from "../../../../hooks";
import {useGetDatesQuery} from "../../../../store/apis";
import {IDate} from "../../../../features/models";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Select} from "../../../ui";
import {dateValidation} from "./validation.ts";
import {useDownloadTimetableReportMutation} from "../../../../store/apis/reportApi.ts";

interface IDateRangeReportForm {
    startDate: IDate
    endDate: IDate
}

export const DateRangeReportForm = () => {
    const {control, handleSubmit, setError, formState: {errors}} = useForm<IDateRangeReportForm>({
        mode: 'onChange',
    })

    const [dateQuery, setDateQuery] = usePaginationQuery()
    const {data: dateData} = useGetDatesQuery({ ...dateQuery, educationalOnly: false })
    const {
        options: dates,
        loadMore: loadMoreDates,
        search: searchDates
    } = useInfinitySelect<IDate>({
        query: dateQuery,
        setQuery: setDateQuery,
        data: dateData
    })

    const [downloadTimetableReport] = useDownloadTimetableReportMutation()

    const onSubmit: SubmitHandler<IDateRangeReportForm> = async data => {
        const startDateId = data.startDate.id
        const endDateId = data.endDate.id

        if (startDateId > endDateId) {
            const error = {
                message: 'Дата начала не может быть больше конечной'
            }

            setError("startDate", error)
            setError("endDate", error)
            return;
        }

        await downloadTimetableReport({
            startDateId: startDateId,
            endDateId: endDateId
        })
    }

    return (
        <Card>
            <Card.Body>
                <Row>
                    <Form onSubmit={handleSubmit(onSubmit)}>
                        <Controller
                            control={control}
                            name='startDate'
                            rules={dateValidation}
                            render={({field}) => (
                                <FormGroup className='my-3'>
                                    <Select
                                        onChange={field.onChange}
                                        onLoadMore={loadMoreDates}
                                        onSearch={searchDates}
                                        value={field.value}
                                        options={dates}
                                        renderValue={(item) => item.value}
                                        label='С'
                                        error={!!errors.startDate?.message}
                                        helperText={errors.startDate?.message}
                                    />
                                </FormGroup>
                            )}
                        />
                        <Controller
                            control={control}
                            name='endDate'
                            rules={dateValidation}
                            render={({field}) => (
                                <FormGroup className='my-3'>
                                    <Select
                                        onChange={field.onChange}
                                        onLoadMore={loadMoreDates}
                                        onSearch={searchDates}
                                        value={field.value}
                                        options={dates}
                                        renderValue={(item) => item.value}
                                        label='По'
                                        error={!!errors.endDate?.message}
                                        helperText={errors.endDate?.message}
                                    />
                                </FormGroup>
                            )}
                        />
                        <Button
                            className='d-flex mx-auto my-3'
                            type='submit'
                        >
                            Сохранить в Excel
                        </Button>
                    </Form>
                </Row>
            </Card.Body>
        </Card>
    )
}