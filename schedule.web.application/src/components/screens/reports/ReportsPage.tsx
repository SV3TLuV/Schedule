import {Container, FormGroup, Row} from "react-bootstrap";
import {Select} from "../../ui";
import {IReportType} from "../../../features/models";
import {Controller, useForm, useWatch} from "react-hook-form";
import {DateRangeReportForm} from "./forms/DateRangeReportForm.tsx";

const reportTypes: IReportType[] = [
    {
        id: 1,
        name: 'Расписание'
    },
]

interface IReportsPageState {
    reportType: IReportType
}

export const ReportsPage = () => {
    const {control, formState: {errors}} = useForm<IReportsPageState>({
        mode: 'onChange',
    })

    const selectedReportType = useWatch({ control, name: 'reportType' })

    function getReportTypeForm(type: IReportType) {
        switch (type.id) {
            case 1:
                return <DateRangeReportForm/>
        }
    }

    return (
        <Container
            style={{
                height: 'calc(100vh - 72px)',
                maxWidth: '480px'
            }}
        >
            <Row className='p-0'>
                <Controller
                    control={control}
                    name='reportType'
                    render={({field}) => (
                        <FormGroup className='my-3'>
                            <Select
                                onChange={field.onChange}
                                value={field.value}
                                options={reportTypes}
                                fields={'name'}
                                label='Вид'
                                error={!!errors.reportType?.message}
                                helperText={errors.reportType?.message}
                            />
                        </FormGroup>
                    )}
                />
            </Row>
            <Row className='p-0 mx-0 my-3'>
                {selectedReportType && getReportTypeForm(selectedReportType)}
            </Row>
        </Container>
    )
}